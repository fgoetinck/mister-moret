namespace MisterMoret.Http.Authentication;

public interface IAccessTokenProvider
{
    string? AccessToken { get; set; }
}