using System.Threading.Tasks;
using MisterMoret.Results;

namespace MisterMoret.Http;

public interface IApiClient
{
    /// <summary>
    /// Sends a GET request to the specified endpoint and deserializes the response.
    /// </summary>
    /// <typeparam name="TResponse">The type to deserialize the response body into.</typeparam>
    /// <param name="endpoint">The relative endpoint path.</param>
    /// <returns>An <see cref="HttpResult{TResponse}"/> representing the outcome of the request.</returns>
    Task<HttpResult<TResponse>> GetAsync<TResponse>(string endpoint);

    /// <summary>
    /// Sends a GET request with query parameters to the specified endpoint and deserializes the response.
    /// </summary>
    /// <typeparam name="TResponse">The type to deserialize the response body into.</typeparam>
    /// <typeparam name="TQuery">The type whose properties are serialized as query string parameters.</typeparam>
    /// <param name="endpoint">The relative endpoint path.</param>
    /// <param name="query">The object whose properties are appended to the request URL as query parameters.</param>
    /// <returns>An <see cref="HttpResult{TResponse}"/> representing the outcome of the request.</returns>
    Task<HttpResult<TResponse>> GetAsync<TResponse, TQuery>(string endpoint, TQuery query);

    /// <summary>
    /// Sends a POST request with a JSON-serialized body to the specified endpoint and deserializes the response.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request body to serialize.</typeparam>
    /// <typeparam name="TResponse">The type to deserialize the response body into.</typeparam>
    /// <param name="endpoint">The relative endpoint path.</param>
    /// <param name="request">The object to serialize as the request body.</param>
    /// <returns>An <see cref="HttpResult{TResponse}"/> representing the outcome of the request.</returns>
    Task<HttpResult<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);

    /// <summary>
    /// Sends a PUT request with a JSON-serialized body to the specified endpoint and deserializes the response.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request body to serialize.</typeparam>
    /// <typeparam name="TResponse">The type to deserialize the response body into.</typeparam>
    /// <param name="endpoint">The relative endpoint path.</param>
    /// <param name="request">The object to serialize as the request body.</param>
    /// <returns>An <see cref="HttpResult{TResponse}"/> representing the outcome of the request.</returns>
    Task<HttpResult<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest request);

    /// <summary>
    /// Sends a DELETE request to the specified endpoint and deserializes the response.
    /// </summary>
    /// <typeparam name="TResponse">The type to deserialize the response body into.</typeparam>
    /// <param name="endpoint">The relative endpoint path.</param>
    /// <returns>An <see cref="HttpResult{TResponse}"/> representing the outcome of the request.</returns>
    Task<HttpResult<TResponse>> DeleteAsync<TResponse>(string endpoint);

    /// <summary>
    /// Sends a DELETE request to the specified endpoint without expecting a response body.
    /// </summary>
    /// <param name="endpoint">The relative endpoint path.</param>
    /// <returns>An <see cref="HttpResult"/> representing the outcome of the request.</returns>
    Task<HttpResult> DeleteAsync(string endpoint);
}