using System.Threading.Tasks;
using MisterMoret.Results;

namespace MisterMoret.Http;

public interface IApiClient
{
    Task<HttpResult<TResponse>> GetAsync<TResponse>(string endpoint);
    Task<HttpResult<TResponse>> GetAsync<TResponse, TQuery>(string endpoint, TQuery query);
    Task<HttpResult<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);
    Task<HttpResult<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest request);
    Task<HttpResult<TResponse>> DeleteAsync<TResponse>(string endpoint);
    Task<HttpResult> DeleteAsync(string endpoint);
}