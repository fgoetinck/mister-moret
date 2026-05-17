using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MisterMoret.Http.Interfaces;

namespace MisterMoret.Http.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiClient(
        this IServiceCollection services,
        string name,
        string baseAddress)
    {
        services.AddHttpClient(name, client =>
        {
            client.BaseAddress = new Uri(baseAddress);
        });
        
        services.TryAddSingleton<IApiClientFactory, ApiClientFactory>();

        return services;
    }
}