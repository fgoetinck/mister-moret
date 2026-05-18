using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MisterMoret.Http.Interfaces;
using MisterMoret.Http.Options;

namespace MisterMoret.Http.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiClient(
        this IServiceCollection services,
        string name,
        Action<ApiClientOptions> configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configuration);

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Client name cannot be empty, or whitespace.", nameof(name));
        
        services.Configure(name, configuration);
        
        services.AddHttpClient<ApiClientOptions>(name, (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptionsMonitor<ApiClientOptions>>().Get(name);

            if (string.IsNullOrWhiteSpace(options.BaseAddress))
                throw new ArgumentException("Base address cannot be empty, or whitespace.",
                    nameof(options.BaseAddress));

            if (!Uri.TryCreate(options.BaseAddress, UriKind.Absolute, out Uri? baseAddressUri))
                throw new ArgumentException("Base address must be a valid absolute URI.", nameof(options.BaseAddress));
            
            client.BaseAddress = baseAddressUri;
            client.Timeout = options.Timeout;
            
            if (!string.IsNullOrWhiteSpace(options.UserAgent))
                client.DefaultRequestHeaders.UserAgent.ParseAdd(options.UserAgent);
            
        });

        services.TryAddSingleton<IApiClientFactory, ApiClientFactory>();

        return services;
    }
}