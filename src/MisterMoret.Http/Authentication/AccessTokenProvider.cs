using System.Collections.Generic;

namespace MisterMoret.Http.Authentication;

/// <summary>
/// An in-memory implementation of <see cref="IAccessTokenProvider"/> that stores access tokens in a dictionary
/// keyed by client name, plus a separate field for the global default token.
/// </summary>
/// <remarks>
/// This class is registered as a scoped service when an authentication scheme is configured via
/// <c>AddApiClient</c>, so token values set within a single scope (e.g. an HTTP request) are visible to all
/// <see cref="AuthenticationHandler"/> instances resolving within that same scope.
/// </remarks>
public class AccessTokenProvider : IAccessTokenProvider
{
    private readonly Dictionary<string, string?> _tokens;
    private string? _token;

    /// <summary>
    /// Initializes a new instance of <see cref="AccessTokenProvider"/> with an empty token store.
    /// </summary>
    public AccessTokenProvider()
    {
        _tokens = new Dictionary<string, string?>();
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