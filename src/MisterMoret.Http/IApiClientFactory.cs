namespace MisterMoret.Http;

/// <summary>
/// Defines a factory for creating <see cref="IApiClient"/> instances resolved by name from the underlying
/// <see cref="System.Net.Http.IHttpClientFactory"/> registration.
/// </summary>
public interface IApiClientFactory
{
    /// <summary>
    /// Creates a named <see cref="IApiClient"/> instance configured for the specified client name.
    /// </summary>
    /// <param name="name">
    /// The name of the registered HTTP client configuration to use. Must match a name previously registered
    /// via <c>AddApiClient(name, ...)</c>.
    /// </param>
    /// <returns>An <see cref="IApiClient"/> backed by the named <see cref="System.Net.Http.HttpClient"/> instance.</returns>
    IApiClient CreateClient(string name);

    /// <summary>
    /// Creates an <see cref="IApiClient"/> instance using the default client configuration registered under
    /// <see cref="MisterMoret.Http.Configuration.ApiClientNames.Default"/>.
    /// </summary>
    /// <returns>An <see cref="IApiClient"/> backed by the default <see cref="System.Net.Http.HttpClient"/> instance.</returns>
    IApiClient CreateClient();
}