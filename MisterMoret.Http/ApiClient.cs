using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using MisterMoret.Http.Interfaces;
using MisterMoret.Results;

namespace MisterMoret.Http
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private const string GetErrorMessage = "Failed to get data.";
        private const string CreateErrorMessage = "Failed to create data.";
        private const string UpdateErrorMessage = "Failed to update data.";
        private const string DeleteErrorMessage = "Failed to delete data.";
        private const string JsonDeserializationErrorMessage = "Failed to deserialize data.";

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResult<TResponse>> GetAsync<TResponse>(string endpoint)
        {
            var url = CreateRelativeEndpoint(endpoint);
            using var response = await _httpClient.GetAsync(url);
            return await HandleHttpResponse<TResponse>(response, GetErrorMessage);
        }

        public async Task<HttpResult<TResponse>> GetAsync<TResponse, TQuery>(string endpoint, TQuery query)
        {
            var url = CreateRelativeEndpoint<TQuery>(endpoint, query);
            using var response = await _httpClient.GetAsync(url);
            return await HandleHttpResponse<TResponse>(response, GetErrorMessage);
        }

        public async Task<HttpResult<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)
        {
            var url = CreateRelativeEndpoint(endpoint);
            using var response = await _httpClient.PostAsJsonAsync(url, request);
            return await HandleHttpResponse<TResponse>(response, CreateErrorMessage);
        }

        public async Task<HttpResult<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest request)
        {
            var url = CreateRelativeEndpoint(endpoint);
            using var response = await _httpClient.PutAsJsonAsync(url, request);
            return await HandleHttpResponse<TResponse>(response, UpdateErrorMessage);
        }

        public async Task<HttpResult<TResponse>> DeleteAsync<TResponse>(string endpoint)
        {
            var url = CreateRelativeEndpoint(endpoint);
            using var response = await _httpClient.DeleteAsync(url);
            return await HandleHttpResponse<TResponse>(response, DeleteErrorMessage);
        }

        public async Task<HttpResult> DeleteAsync(string endpoint)
        {
            var url = CreateRelativeEndpoint(endpoint);
            using var response = await _httpClient.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return HttpResult.Failure(DeleteErrorMessage, response.StatusCode);
            }

            return HttpResult.Success();
        }

        private static async Task<HttpResult<TResponse>> HandleHttpResponse<TResponse>(HttpResponseMessage response,
            string errorMessage)
        {
            if (!response.IsSuccessStatusCode)
            {
                return HttpResult<TResponse>.Failure(errorMessage, response.StatusCode);
            }

            var data = await response.Content.ReadFromJsonAsync<TResponse>(JsonSerializerOptions);
            if (data == null)
            {
                return HttpResult<TResponse>.Failure(JsonDeserializationErrorMessage, response.StatusCode);
            }

            return HttpResult<TResponse>.Success(data);
        }

        private string CreateRelativeEndpoint(string endpoint)
        {
            string trimmedEndpoint = endpoint.Trim('/');

            if (!_httpClient.BaseAddress.ToString().EndsWith('/'))
            {
                trimmedEndpoint = "/" + trimmedEndpoint;
            }

            return trimmedEndpoint;
        }
        
        private string CreateRelativeEndpoint<TQuery>(string endpoint, TQuery query)
        {
            var uriQuery = new Dictionary<string, string>();
            for (int i = 0; i < typeof(TQuery).GetProperties().Length; i++)
            {
                var property = typeof(TQuery).GetProperties()[i];
                if (property.GetValue(query) != null)
                {
                    uriQuery.Add(property.Name, property.GetValue(query)?.ToString() ?? string.Empty);
                }
            }

            endpoint = CreateRelativeEndpoint(endpoint);
            
            return QueryHelpers.AddQueryString(endpoint, uriQuery);
        }
    }
}