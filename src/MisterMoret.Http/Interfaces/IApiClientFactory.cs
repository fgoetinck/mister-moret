namespace MisterMoret.Http.Interfaces;

public interface IApiClientFactory
{
    IApiClient CreateClient(string name);
}