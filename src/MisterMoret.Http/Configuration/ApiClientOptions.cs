using System;

namespace MisterMoret.Http.Options;

public class ApiClientOptions
{
    public string BaseAddress { get; set; } = string.Empty;
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);
    public string? UserAgent { get; set; }
}