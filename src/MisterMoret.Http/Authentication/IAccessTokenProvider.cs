namespace MisterMoret.Http.Authentication;

public interface IAccessTokenProvider
{
    /// <summary>
    /// Gets the access token for the specified client.
    /// </summary>
    /// <param name="clientName">The name of the client.</param>
    /// <returns>The access token for the named client, or <see langword="null"/> if none has been set.</returns>
    string? GetAccessToken(string clientName);

    /// <summary>
    /// Gets the access token for the default client.
    /// </summary>
    /// <returns>The access token for the default client, or <see langword="null"/> if none has been set.</returns>
    string? GetAccessToken();
    
    /// <summary>
    /// Sets the access token for the specified client.
    /// </summary>
    /// <param name="clientName">The name of the client.</param>
    /// <param name="accessToken">The access token to set.</param>
    void SetAccessToken(string clientName, string accessToken);
    
    /// <summary>
    /// Sets the access token for the default client.
    /// </summary>
    /// <param name="accessToken">The access token to set.</param>
    void SetAccessToken(string accessToken);
}