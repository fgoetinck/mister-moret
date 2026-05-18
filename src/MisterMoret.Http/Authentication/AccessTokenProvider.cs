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

    public string? GetAccessToken(string clientName) =>
        _tokens.GetValueOrDefault(clientName);

    public string? GetAccessToken() =>
        _token;

    public void SetAccessToken(string clientName, string accessToken) =>
        _tokens[clientName] = accessToken;

    public void SetAccessToken(string accessToken) =>
        _token = accessToken;
}