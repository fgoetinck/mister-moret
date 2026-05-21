using System.Collections.Generic;

namespace MisterMoret.Http.Authentication;

public class AccessTokenProvider : IAccessTokenProvider
{
    private readonly Dictionary<string, string?> _tokens;
    private string? _token;

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