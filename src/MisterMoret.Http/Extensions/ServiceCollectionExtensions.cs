using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MisterMoret.Http.Authentication;
using MisterMoret.Http.Configuration;

namespace MisterMoret.Http.Extensions;

/// <summary>
/// Provides extension methods on <see cref="IServiceCollection"/> for registering API clients and their
/// supporting services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a named API client with the dependency injection container, configuring the underlying
    /// <see cref="System.Net.Http.HttpClient"/> from <paramref name="configuration"/> and optionally
    /// adding bearer token authentication.
    /// </summary>
    /// <param name="services">The service collection to register the client into. Must not be <see langword="null"/>.</param>
    /// <param name="name">
    /// The unique name used to identify and resolve this client. Must not be <see langword="null"/>, empty,
    /// or whitespace.
    /// </param>
    /// <param name="configuration">
    /// A delegate that populates the <see cref="ApiClientOptions"/> for this client. Must not be
    /// <see langword="null"/>. The <see cref="ApiClientOptions.BaseAddress"/> must be set to a valid absolute URI.
    /// </param>
    /// <param name="authenticationScheme">
    /// When provided, registers an <see cref="AuthenticationHandler"/> that reads a token stored under
    /// <paramref name="name"/> from <see cref="IAccessTokenProvider"/> and attaches it as an
    /// <c>Authorization</c> header using this scheme (e.g. <c>"Bearer"</c>) on every outgoing request.
    /// <see cref="IAccessTokenProvider"/> is registered as a scoped service if not already present.
    /// </param>
    /// <returns>The original <paramref name="services"/> instance to allow call chaining.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="services"/> or <paramref name="configuration"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is <see langword="null"/>, empty, or whitespace, or when the
    /// configured <see cref="ApiClientOptions.BaseAddress"/> is empty or not a valid absolute URI.
    /// </exception>
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
    /// Registers the default API client with the dependency injection container under
    /// <see cref="ApiClientNames.Default"/>, configuring the underlying <see cref="System.Net.Http.HttpClient"/>
    /// from <paramref name="configuration"/> and optionally adding bearer token authentication.
    /// </summary>
    /// <param name="services">The service collection to register the client into. Must not be <see langword="null"/>.</param>
    /// <param name="configuration">
    /// A delegate that populates the <see cref="ApiClientOptions"/> for the default client. Must not be
    /// <see langword="null"/>. The <see cref="ApiClientOptions.BaseAddress"/> must be set to a valid absolute URI.
    /// </param>
    /// <param name="authenticationScheme">
    /// When provided, registers an <see cref="AuthenticationHandler"/> that reads the global default token from
    /// <see cref="IAccessTokenProvider"/> and attaches it as an <c>Authorization</c> header using this scheme
    /// (e.g. <c>"Bearer"</c>) on every outgoing request. <see cref="IAccessTokenProvider"/> is registered as a
    /// scoped service if not already present.
    /// </param>
    /// <returns>The original <paramref name="services"/> instance to allow call chaining.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="services"/> or <paramref name="configuration"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the configured <see cref="ApiClientOptions.BaseAddress"/> is empty or not a valid absolute URI.
    /// </exception>
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