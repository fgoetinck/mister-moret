namespace MisterMoret.Http.Configuration;

/// <summary>
/// Provides well-known name constants for API clients registered with the dependency injection container.
/// </summary>
public class ApiClientNames
{
    /// <summary>
    /// The name used for the default API client registered without an explicit name via
    /// <c>AddApiClient(options, scheme?)</c>.
    /// </summary>
    public const string Default = "default";
}