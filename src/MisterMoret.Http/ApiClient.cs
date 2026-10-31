using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using MisterMoret.Results;

namespace MisterMoret.Http;

/// <summary>
/// A sealed, typed HTTP client that executes REST verb operations against a pre-configured base address and maps
/// HTTP responses to <see cref="HttpResult{T}"/> or <see cref="HttpResult"/> values.
/// </summary>
/// <remarks>
/// On a non-success HTTP response, each method attempts to read the <c>errors</c> member of a JSON response body, either
/// an array of strings or the ASP.NET Core <c>ValidationProblemDetails</c> form (an object of string arrays, flattened into
/// one list). When one or more errors are present they are forwarded to the returned <see cref="HttpResult{T}"/> failure.
/// Otherwise (no errors, or an empty, malformed or non-JSON body) a generic fallback message is used. The HTTP status code
/// is preserved on non-success results. When a success response body cannot be deserialized (empty, invalid JSON, non-JSON
/// content type or unsupported charset) the result is a failure with the message "Failed to deserialize data." and status
/// code 500, so a failed result never carries a 2xx code. Other exceptions, such as a <see cref="NotSupportedException"/>
/// for an unsupported <c>TResponse</c>, are not caught.
/// Instances are created exclusively by <see cref="ApiClientFactory"/> and cannot be subclassed.
/// </remarks>
public sealed class ApiClient : IApiClient
{
    private const string GetErrorMessage = "Failed to get data.";
    private const string CreateErrorMessage = "Failed to create data.";
    private const string UpdateErrorMessage = "Failed to update data.";
    private const string DeleteErrorMessage = "Failed to delete data.";
    private const string JsonDeserializationErrorMessage = "Failed to deserialize data.";

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;

    internal ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<HttpResult<TResponse>> GetAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint);
        using var response = await _httpClient.GetAsync(url, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, GetErrorMessage, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<HttpResult<TResponse>> GetAsync<TResponse, TQuery>(string endpoint, TQuery query, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint, query);
        using var response = await _httpClient.GetAsync(url, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, GetErrorMessage, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<HttpResult<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint);
        using var response = await _httpClient.PostAsJsonAsync(url, request, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, CreateErrorMessage, cancellationToken);
    }
    
    /// <inheritdoc/>
    public async Task<HttpResult<TResponse>> PostAsync<TResponse>(string endpoint, HttpContent content, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint);
        using var response = await _httpClient.PostAsync(url, content, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, CreateErrorMessage, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<HttpResult> PostAsync<TRequest>(string endpoint, TRequest request, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint);
        using var response = await _httpClient.PostAsJsonAsync(url, request, cancellationToken);
        if (!response.IsSuccessStatusCode) return HttpResult.Failure(CreateErrorMessage, response.StatusCode);
        return HttpResult.Success();
    }

    /// <inheritdoc/>
    public async Task<HttpResult<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint);
        using var response = await _httpClient.PutAsJsonAsync(url, request, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, UpdateErrorMessage, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<HttpResult> PutAsync<TRequest>(string endpoint, TRequest request, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint);
        using var response = await _httpClient.PutAsJsonAsync(url, request, cancellationToken);
        if (!response.IsSuccessStatusCode) return HttpResult.Failure(UpdateErrorMessage, response.StatusCode);
        return HttpResult.Success();
    }

    /// <inheritdoc/>
    public async Task<HttpResult<TResponse>> PatchAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint);
        using var response = await _httpClient.PatchAsJsonAsync(url, request, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, UpdateErrorMessage, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<HttpResult> PatchAsync<TRequest>(string endpoint, TRequest request, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint);
        using var response = await _httpClient.PatchAsJsonAsync(url, request, cancellationToken);
        if (!response.IsSuccessStatusCode) return HttpResult.Failure(UpdateErrorMessage, response.StatusCode);
        return HttpResult.Success();
    }

    /// <inheritdoc/>
    public async Task<HttpResult<TResponse>> DeleteAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint);
        using var response = await _httpClient.DeleteAsync(url, cancellationToken);
        return await HandleHttpResponse<TResponse>(response, DeleteErrorMessage, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<HttpResult> DeleteAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        string url = CreateRelativeEndpoint(endpoint);
        using var response = await _httpClient.DeleteAsync(url, cancellationToken);
        if (!response.IsSuccessStatusCode) return HttpResult.Failure(DeleteErrorMessage, response.StatusCode);

        return HttpResult.Success();
    }

    private sealed record ApiErrorResponse(JsonElement? Errors);

    private static async Task<HttpResult<TResponse>> HandleHttpResponse<TResponse>(HttpResponseMessage response,
        string errorMessage, CancellationToken cancellationToken)
    {
        bool readable = IsReadableJson(response.Content);

        if (!response.IsSuccessStatusCode)
        {
            ApiErrorResponse? apiError = null;
            if (readable)
            {
                try
                {
                    apiError = await response.Content.ReadFromJsonAsync<ApiErrorResponse>(JsonSerializerOptions, cancellationToken);
                }
                catch (JsonException)
                {
                }
            }

            var errors = ExtractErrors(apiError?.Errors);
            if (errors.Count > 0)
                return HttpResult<TResponse>.Failure(errors, response.StatusCode);

            return HttpResult<TResponse>.Failure(errorMessage, response.StatusCode);
        }

        if (!readable)
            return HttpResult<TResponse>.Failure(JsonDeserializationErrorMessage, HttpStatusCode.InternalServerError);

        TResponse? data;
        try
        {
            data = await response.Content.ReadFromJsonAsync<TResponse>(JsonSerializerOptions, cancellationToken);
        }
        catch (JsonException ex)
        {
            return HttpResult<TResponse>.Failure([JsonDeserializationErrorMessage, ex.Message], HttpStatusCode.InternalServerError);
        }

        if (data == null)
            return HttpResult<TResponse>.Failure(JsonDeserializationErrorMessage, HttpStatusCode.InternalServerError);

        return HttpResult<TResponse>.Success(data);
    }

    // ReadFromJsonAsync throws for empty bodies, non-JSON types and unknown charsets, so check first.
    private static bool IsReadableJson(HttpContent content)
    {
        if (content.Headers.ContentLength == 0) return false;

        var contentType = content.Headers.ContentType;
        string? mediaType = contentType?.MediaType;
        if (mediaType == null) return false;
        if (!mediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase) &&
            !mediaType.EndsWith("+json", StringComparison.OrdinalIgnoreCase)) return false;

        if (string.IsNullOrEmpty(contentType!.CharSet)) return true;
        try
        {
            _ = Encoding.GetEncoding(contentType.CharSet);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    // Accepts "errors": ["..."] and the ValidationProblemDetails form "errors": {"Field": ["..."]}.
    private static List<string> ExtractErrors(JsonElement? element)
    {
        var errors = new List<string>();
        if (element is not { } value) return errors;

        switch (value.ValueKind)
        {
            case JsonValueKind.Array:
                AddStrings(value, errors);
                break;
            case JsonValueKind.Object:
                foreach (var property in value.EnumerateObject())
                {
                    if (property.Value.ValueKind == JsonValueKind.Array) AddStrings(property.Value, errors);
                    else if (property.Value.ValueKind == JsonValueKind.String) AddString(property.Value, errors);
                }
                break;
        }

        return errors;
    }

    private static void AddStrings(JsonElement array, List<string> errors)
    {
        foreach (var item in array.EnumerateArray()) AddString(item, errors);
    }

    private static void AddString(JsonElement item, List<string> errors)
    {
        if (item.ValueKind == JsonValueKind.String && item.GetString() is { Length: > 0 } text) errors.Add(text);
    }

    private string CreateRelativeEndpoint(string endpoint)
    {
        string trimmedEndpoint = endpoint.Trim('/');

        if (!_httpClient.BaseAddress!.ToString().EndsWith('/')) trimmedEndpoint = "/" + trimmedEndpoint;

        return trimmedEndpoint;
    }

    private string CreateRelativeEndpoint<TQuery>(string endpoint, TQuery query)
    {
        var uriQuery = new Dictionary<string, string?>();
        var queryProperties = typeof(TQuery).GetProperties();
        for (var i = 0; i < queryProperties.Length; i++)
        {
            var property = queryProperties[i];
            if (property.GetValue(query) != null)
                uriQuery.Add(property.Name, property.GetValue(query)?.ToString() ?? string.Empty);
        }

        endpoint = CreateRelativeEndpoint(endpoint);

        return QueryHelpers.AddQueryString(endpoint, uriQuery);
    }
}