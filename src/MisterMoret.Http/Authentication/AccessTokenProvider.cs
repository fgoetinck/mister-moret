using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MisterMoret.Http.Authentication;

/// <summary>
/// An in-memory implementation of <see cref="IAccessTokenProvider"/> that stores access tokens in a dictionary
/// keyed by client name, plus a separate field for the global default token.
/// </summary>
/// <remarks>
/// This class is registered as a singleton service when an authentication scheme is configured via
/// <c>AddApiClient</c>. All members are thread-safe and can be called concurrently from multiple threads.
/// </remarks>
public class AccessTokenProvider : IAccessTokenProvider
{
    private readonly ConcurrentDictionary<string, string?> _tokens;
    private volatile string? _token;

    /// <summary>
    /// Initializes a new instance of <see cref="AccessTokenProvider"/> with an empty token store.
    /// </summary>
    public AccessTokenProvider()
    {
        _tokens = new ConcurrentDictionary<string, string?>();
    }

    /// <inheritdoc/>
    public string? GetAccessToken(string clientName) =>
        _tokens.GetValueOrDefault(clientName);

    /// <inheritdoc/>
    public string? GetAccessToken() =>
        _token;

    /// <inheritdoc/>
    public void SetAccessToken(string clientName, string accessToken) =>
        _tokens[clientName] = accessToken;

    /// <inheritdoc/>
    public void SetAccessToken(string accessToken) =>
        _token = accessToken;
}