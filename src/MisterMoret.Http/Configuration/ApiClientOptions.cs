using System;

namespace MisterMoret.Http.Configuration;

public class ApiClientOptions
{
    /// <summary>
    /// The base address of the API. Must be a valid absolute URI.
    /// </summary>
    public string BaseAddress { get; set; } = string.Empty;

    /// <summary>
    /// The duration to wait before the request times out. Defaults to 100 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

    /// <summary>
    /// The value to use for the User-Agent request header. When null or empty, no User-Agent header is sent.
    /// </summary>
    public string? UserAgent { get; set; }
}