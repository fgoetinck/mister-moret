using System;

namespace MisterMoret.Http.Configuration;

/// <summary>
/// Holds configuration values applied to a registered <see cref="System.Net.Http.HttpClient"/> instance
/// when an API client is added via <c>AddApiClient</c>.
/// </summary>
public class ApiClientOptions
{
    /// <summary>
    /// Gets or sets the base address of the API. Must be a valid absolute URI.
    /// </summary>
    /// <remarks>
    /// This value is required. Registration will throw <see cref="ArgumentException"/> at startup if it is
    /// null, empty, whitespace, or not a well-formed absolute URI.
    /// </remarks>
    public string BaseAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the duration the underlying <see cref="System.Net.Http.HttpClient"/> waits before timing
    /// out a request. Defaults to 100 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

    /// <summary>
    /// Gets or sets the value sent as the <c>User-Agent</c> request header. When <see langword="null"/> or
    /// empty, no <c>User-Agent</c> header is added to outgoing requests.
    /// </summary>
    public string? UserAgent { get; set; }
}