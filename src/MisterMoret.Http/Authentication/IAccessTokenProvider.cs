namespace MisterMoret.Http.Authentication;

/// <summary>
/// Defines storage and retrieval of bearer access tokens, keyed either by client name or as a global default,
/// for use by <see cref="AuthenticationHandler"/> when injecting authorization headers into outgoing requests.
/// </summary>
public interface IAccessTokenProvider
{
    /// <summary>
    /// Retrieves the access token stored for the specified client name.
    /// </summary>
    /// <param name="clientName">The name of the client whose token should be retrieved.</param>
    /// <returns>
    /// The access token previously set for <paramref name="clientName"/>, or <see langword="null"/> if no token
    /// has been stored for that client.
    /// </returns>
    string? GetAccessToken(string clientName);

    /// <summary>
    /// Retrieves the global default access token, used when no client-specific token is required.
    /// </summary>
    /// <returns>
    /// The global access token previously set via <see cref="SetAccessToken(string)"/>, or
    /// <see langword="null"/> if none has been set.
    /// </returns>
    string? GetAccessToken();

    /// <summary>
    /// Stores an access token for the specified client name, replacing any previously stored value.
    /// </summary>
    /// <param name="clientName">The name of the client to associate the token with.</param>
    /// <param name="accessToken">The access token to store. Must not be <see langword="null"/>.</param>
    void SetAccessToken(string clientName, string accessToken);

    /// <summary>
    /// Stores a global default access token, replacing any previously stored global value.
    /// </summary>
    /// <param name="accessToken">The access token to store. Must not be <see langword="null"/>.</param>
    void SetAccessToken(string accessToken);
}