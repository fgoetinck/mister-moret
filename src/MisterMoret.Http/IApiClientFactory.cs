namespace MisterMoret.Http;

public interface IApiClientFactory
{
    IApiClient CreateClient(string name);
    IApiClient CreateClient();
}