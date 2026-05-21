using System.Net.Http;
using MisterMoret.Http.Configuration;

namespace MisterMoret.Http;

public class ApiClientFactory : IApiClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiClientFactory(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <inheritdoc/>
    public IApiClient CreateClient(string name)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(name);
        return new ApiClient(httpClient);
    }
    
    /// <inheritdoc/>
    public IApiClient CreateClient() => CreateClient(ApiClientNames.Default);
}