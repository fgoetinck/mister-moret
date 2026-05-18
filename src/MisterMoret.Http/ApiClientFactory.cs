using System.Net.Http;

namespace MisterMoret.Http;

public class ApiClientFactory : IApiClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiClientFactory(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IApiClient CreateClient(string name)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(name);
        return new ApiClient(httpClient);
    }
}