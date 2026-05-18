using System.Collections.Generic;

namespace MisterMoret.Http.Authentication;

public class AccessTokenProvider : IAccessTokenProvider
{
    private readonly Dictionary<string, string?> _tokens = new();
    public string? GetAccessToken(string clientName)
    {
        return _tokens.GetValueOrDefault(clientName);
    }

    public void SetAccessToken(string clientName, string accessToken)
    {
        _tokens[clientName] = accessToken;
    }
}