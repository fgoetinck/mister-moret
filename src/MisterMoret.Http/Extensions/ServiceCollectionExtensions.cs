using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MisterMoret.Http.Authentication;
using MisterMoret.Http.Configuration;

namespace MisterMoret.Http.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a named API client with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to register the client into.</param>
    /// <param name="name">The unique name used to identify and resolve this client.</param>
    /// <param name="configuration">A delegate that configures the <see cref="ApiClientOptions"/> for this client.</param>
    /// <param name="authenticationScheme">
    /// When provided, registers an <see cref="AuthenticationHandler"/> that attaches a bearer token
    /// for the given scheme to each outgoing request.
    /// </param>
    /// <returns>The original <paramref name="services"/> instance to allow chaining.</returns>
    public static IServiceCollection AddApiClient(this IServiceCollection services, string name,
        Action<ApiClientOptions> configuration, string? authenticationScheme = null)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configuration);

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Client name cannot be empty, or whitespace.", nameof(name));

        services.Configure(name, configuration);

        var httpClientBuilder = services.AddHttpClient(name, (sp, client) =>
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

        if (!string.IsNullOrWhiteSpace(authenticationScheme))
        {
            services.TryAddScoped<IAccessTokenProvider, AccessTokenProvider>();

            httpClientBuilder.AddHttpMessageHandler(sp => new AuthenticationHandler(
                sp.GetRequiredService<IAccessTokenProvider>(),
                authenticationScheme,
                name));
        }

        services.TryAddSingleton<IApiClientFactory, ApiClientFactory>();

        return services;
    }
    
    /// <summary>
    /// Registers the default API client with the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to register the client into.</param>
    /// <param name="configuration">A delegate that configures the <see cref="ApiClientOptions"/> for the default client.</param>
    /// <param name="authenticationScheme">
    /// When provided, registers an <see cref="AuthenticationHandler"/> that attaches a bearer token
    /// for the given scheme to each outgoing request.
    /// </param>
    /// <returns>The original <paramref name="services"/> instance to allow chaining.</returns>
    public static IServiceCollection AddApiClient(this IServiceCollection services,
        Action<ApiClientOptions> configuration, string? authenticationScheme = null)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.Configure(configuration);

        var httpClientBuilder = services.AddHttpClient(ApiClientNames.Default, (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<ApiClientOptions>>().Value;

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

        if (!string.IsNullOrWhiteSpace(authenticationScheme))
        {
            services.TryAddScoped<IAccessTokenProvider, AccessTokenProvider>();

            httpClientBuilder.AddHttpMessageHandler(sp => new AuthenticationHandler(
                sp.GetRequiredService<IAccessTokenProvider>(),
                authenticationScheme));
        }

        services.TryAddSingleton<IApiClientFactory, ApiClientFactory>();

        return services;
    }
}