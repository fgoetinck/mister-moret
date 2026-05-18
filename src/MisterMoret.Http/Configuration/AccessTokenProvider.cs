using MisterMoret.Http.Configuration.Interfaces;

namespace MisterMoret.Http.Configuration;

public class AccessTokenProvider : IAccessTokenProvider
{
    public string? AccessToken { get; set; }
}