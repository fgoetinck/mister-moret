namespace MisterMoret.Http.Authentication;

public interface IAccessTokenProvider
{
    string? GetAccessToken(string clientName);
    public string? GetAccessToken();
    void SetAccessToken(string clientName, string accessToken);
    void SetAccessToken(string accessToken);
}