using System.Threading;
using System.Threading.Tasks;
using MisterMoret.Results;

namespace MisterMoret.Http;

/// <summary>
/// Defines HTTP verb operations for a configured API endpoint, each returning a typed <see cref="HttpResult{T}"/>
/// that captures both the HTTP status code and the success or failure outcome of the request.
/// </summary>
public interface IApiClient
{
    /// <summary>
    /// Sends a GET request to the specified endpoint and deserializes a successful response body into
    /// <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TResponse">The type to deserialize the response body into on a successful response.</typeparam>
    /// <param name="endpoint">The relative path of the endpoint, resolved against the client's configured base address.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Defaults to <see cref="CancellationToken.None"/> when not supplied.</param>
    /// <returns>
    /// A successful <see cref="HttpResult{TResponse}"/> containing the deserialized value when the server returns a
    /// success status code and the body can be deserialized. Returns a failed <see cref="HttpResult{TResponse}"/>
    /// when the server returns a non-success status code — carrying the server's own error details if the response
    /// body can be deserialized as <see cref="HttpResult{TResponse}"/>, or a generic failure message otherwise.
    /// </returns>
    Task<HttpResult<TResponse>> GetAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a GET request with query string parameters derived from <paramref name="query"/> to the specified
    /// endpoint and deserializes a successful response body into <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TResponse">The type to deserialize the response body into on a successful response.</typeparam>
    /// <typeparam name="TQuery">
    /// The type whose public properties are reflected and appended to the request URL as query string parameters.
    /// Properties with a <see langword="null"/> value are omitted from the query string.
    /// </typeparam>
    /// <param name="endpoint">The relative path of the endpoint, resolved against the client's configured base address.</param>
    /// <param name="query">
    /// The object whose public property values are serialized as query string parameters. Must not be <see langword="null"/>.
    /// </param>
    /// <param name="cancellationToken">A token to cancel the operation. Defaults to <see cref="CancellationToken.None"/> when not supplied.</param>
    /// <returns>
    /// A successful <see cref="HttpResult{TResponse}"/> containing the deserialized value when the server returns a
    /// success status code and the body can be deserialized. Returns a failed <see cref="HttpResult{TResponse}"/>
    /// when the server returns a non-success status code — carrying the server's own error details if the response
    /// body can be deserialized as <see cref="HttpResult{TResponse}"/>, or a generic failure message otherwise.
    /// </returns>
    Task<HttpResult<TResponse>> GetAsync<TResponse, TQuery>(string endpoint, TQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a POST request with <paramref name="request"/> serialized as a JSON body to the specified endpoint
    /// and deserializes a successful response body into <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TRequest">The type of the object to serialize as the JSON request body.</typeparam>
    /// <typeparam name="TResponse">The type to deserialize the response body into on a successful response.</typeparam>
    /// <param name="endpoint">The relative path of the endpoint, resolved against the client's configured base address.</param>
    /// <param name="request">The object to serialize as the JSON request body. Must not be <see langword="null"/>.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Defaults to <see cref="CancellationToken.None"/> when not supplied.</param>
    /// <returns>
    /// A successful <see cref="HttpResult{TResponse}"/> containing the deserialized value when the server returns a
    /// success status code and the body can be deserialized. Returns a failed <see cref="HttpResult{TResponse}"/>
    /// when the server returns a non-success status code — carrying the server's own error details if the response
    /// body can be deserialized as <see cref="HttpResult{TResponse}"/>, or a generic failure message otherwise.
    /// </returns>
    Task<HttpResult<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a PUT request with <paramref name="request"/> serialized as a JSON body to the specified endpoint
    /// and deserializes a successful response body into <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TRequest">The type of the object to serialize as the JSON request body.</typeparam>
    /// <typeparam name="TResponse">The type to deserialize the response body into on a successful response.</typeparam>
    /// <param name="endpoint">The relative path of the endpoint, resolved against the client's configured base address.</param>
    /// <param name="request">The object to serialize as the JSON request body. Must not be <see langword="null"/>.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Defaults to <see cref="CancellationToken.None"/> when not supplied.</param>
    /// <returns>
    /// A successful <see cref="HttpResult{TResponse}"/> containing the deserialized value when the server returns a
    /// success status code and the body can be deserialized. Returns a failed <see cref="HttpResult{TResponse}"/>
    /// when the server returns a non-success status code — carrying the server's own error details if the response
    /// body can be deserialized as <see cref="HttpResult{TResponse}"/>, or a generic failure message otherwise.
    /// </returns>
    Task<HttpResult<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a DELETE request to the specified endpoint and deserializes a successful response body into
    /// <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TResponse">The type to deserialize the response body into on a successful response.</typeparam>
    /// <param name="endpoint">The relative path of the endpoint, resolved against the client's configured base address.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Defaults to <see cref="CancellationToken.None"/> when not supplied.</param>
    /// <returns>
    /// A successful <see cref="HttpResult{TResponse}"/> containing the deserialized value when the server returns a
    /// success status code and the body can be deserialized. Returns a failed <see cref="HttpResult{TResponse}"/>
    /// when the server returns a non-success status code — carrying the server's own error details if the response
    /// body can be deserialized as <see cref="HttpResult{TResponse}"/>, or a generic failure message otherwise.
    /// </returns>
    Task<HttpResult<TResponse>> DeleteAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a DELETE request to the specified endpoint without expecting a deserializable response body.
    /// </summary>
    /// <param name="endpoint">The relative path of the endpoint, resolved against the client's configured base address.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Defaults to <see cref="CancellationToken.None"/> when not supplied.</param>
    /// <returns>
    /// A successful <see cref="HttpResult"/> when the server returns a success status code.
    /// A failed <see cref="HttpResult"/> carrying a generic error message and the HTTP status code when the server
    /// returns a non-success status code.
    /// </returns>
    Task<HttpResult> DeleteAsync(string endpoint, CancellationToken cancellationToken = default);
}