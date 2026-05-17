using System;
using Microsoft.Extensions.DependencyInjection;
using MisterMoret.Http.Interfaces;

namespace MisterMoret.Http.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiClient(
        this IServiceCollection services,
        string baseAddress)
    {
        services.AddHttpClient<IApiClient, ApiClient>(client => { client.BaseAddress = new Uri(baseAddress); });

        return services;
    }
}