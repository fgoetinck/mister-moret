using System.Collections.Generic;

namespace MisterMoret.Http.Authentication;

public class AccessTokenProvider : IAccessTokenProvider
{
    private readonly Dictionary<string, string?> _tokens = new();
    private string? _token;

    public string? GetAccessToken(string clientName) =>
        _tokens.GetValueOrDefault(clientName);

    public string? GetAccessToken() =>
        _token;

    public void SetAccessToken(string clientName, string accessToken) =>
        _tokens[clientName] = accessToken;

    public void SetAccessToken(string accessToken) =>
        _token = accessToken;
}