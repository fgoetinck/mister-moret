namespace MisterMoret.Http.Authentication;

public interface IAccessTokenProvider
{
    string? GetAccessToken(string clientName);
    void SetAccessToken(string clientName, string accessToken);
}