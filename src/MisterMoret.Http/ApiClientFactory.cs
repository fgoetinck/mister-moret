using System.Net.Http;
using MisterMoret.Http.Configuration;

namespace MisterMoret.Http;

/// <summary>
/// Creates <see cref="IApiClient"/> instances by resolving named <see cref="HttpClient"/> instances from the
/// underlying <see cref="IHttpClientFactory"/>.
/// </summary>
public class ApiClientFactory : IApiClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initializes a new instance of <see cref="ApiClientFactory"/> with the supplied HTTP client factory.
    /// </summary>
    /// <param name="httpClientFactory">
    /// The factory used to create <see cref="HttpClient"/> instances by name. Must not be <see langword="null"/>.
    /// </param>
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