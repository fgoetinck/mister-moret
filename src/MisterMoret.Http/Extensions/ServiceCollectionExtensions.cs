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
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(baseAddress);
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Client name cannot be empty, or whitespace.", nameof(name));

        if (string.IsNullOrWhiteSpace(baseAddress))
            throw new ArgumentException("Base address cannot be empty, or whitespace.", nameof(baseAddress));

        if (!Uri.TryCreate(baseAddress, UriKind.Absolute, out Uri? baseAddressUri))
            throw new ArgumentException("Base address must be a valid absolute URI.", nameof(baseAddress));

        services.AddHttpClient(name, client =>
        {
            client.BaseAddress = baseAddressUri;
        });
        
        services.TryAddSingleton<IApiClientFactory, ApiClientFactory>();

        return services;
    }
}