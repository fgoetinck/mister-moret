namespace MisterMoret.Http;

public interface IApiClientFactory
{
    /// <summary>
    /// Creates a named <see cref="IApiClient"/> instance configured for the specified client name.
    /// </summary>
    /// <param name="name">The name of the client configuration to use.</param>
    /// <returns>An <see cref="IApiClient"/> configured for the given name.</returns>
    IApiClient CreateClient(string name);

    /// <summary>
    /// Creates an <see cref="IApiClient"/> instance using the default client configuration.
    /// </summary>
    /// <returns>An <see cref="IApiClient"/> configured with the default settings.</returns>
    IApiClient CreateClient();
}