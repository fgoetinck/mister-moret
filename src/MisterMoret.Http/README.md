# MisterMoret.Http

![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)
[![NuGet](https://img.shields.io/nuget/vpre/MisterMoret.Http.svg)](https://www.nuget.org/packages/MisterMoret.Http)
![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512bd4.svg?logo=dotnet)
![.NET 9.0](https://img.shields.io/badge/.NET-9.0-512bd4.svg?logo=dotnet)
![.NET 10.0](https://img.shields.io/badge/.NET-10.0-512bd4.svg?logo=dotnet)
[![NuGet](https://img.shields.io/badge/NuGet-fgoetinck-blue.svg?logo=nuget)](https://www.nuget.org/profiles/fgoetinck)
[![GitHub](https://img.shields.io/badge/GitHub-fgoetinck-181717.svg?logo=github)](https://github.com/fgoetinck)

A simple and extensible **API client wrapper** for .NET, built on top of `IHttpClientFactory` and integrated with `MisterMoret.Results` for robust error handling and status code management.

> [!NOTE]
> This package is currently in **beta** and available via [NuGet.org](https://www.nuget.org/).

## ✨ Features

- **Named API Clients**: Easily register and use multiple API clients via `IApiClientFactory`.
- **Typed Responses**: Automatic JSON (de)serialization into typed objects.
- **Result Pattern Integration**: Returns `HttpResult<T>` instead of throwing exceptions for non-success status codes. When the server returns a structured `HttpResult` error body, its errors are surfaced directly; otherwise a generic failure is returned.
- **Query Parameter Support**: Simplified way to pass query parameters via anonymous objects or classes.
- **Cancellation Support**: Every HTTP verb method accepts an optional `CancellationToken` as its last parameter.
- **Authentication Support**: Built-in bearer token injection via `IAccessTokenProvider`, designed for global/machine tokens (e.g. client credentials, API keys). Extensible for per-user scenarios via a custom `IAccessTokenProvider`.
- **Configurable Options**: Control base address, timeout, and user-agent through `ApiClientOptions`.
- **Dependency Injection Ready**: Seamlessly integrates with `IServiceCollection`.
- **Modern .NET Support**: Targets **.NET 8.0, 9.0, and 10.0**.

## 🚀 Installation

Install the package via the NuGet CLI:

```bash
dotnet add package MisterMoret.Http --version 1.0.0-beta.9
```

## 💡 Usage

### 1. Registration

Register your API client in `Program.cs` or `Startup.cs`:

```csharp
using MisterMoret.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register a named client
builder.Services.AddApiClient("MyService", options =>
{
    options.BaseAddress = "https://api.example.com/v1/";
    options.Timeout = TimeSpan.FromSeconds(30); // optional, default is 100s
    options.UserAgent = "MyApp/1.0";            // optional
});

// Register a default (unnamed) client
builder.Services.AddApiClient(options =>
{
    options.BaseAddress = "https://api.example.com/v1/";
});
```

### 2. Basic Usage

Inject `IApiClientFactory` and create a client by name:

```csharp
using MisterMoret.Http;

public class MyService
{
    private readonly IApiClientFactory _apiClientFactory;

    public MyService(IApiClientFactory apiClientFactory)
    {
        _apiClientFactory = apiClientFactory;
    }

    public async Task<string?> GetUserName(int userId, CancellationToken cancellationToken = default)
    {
        var client = _apiClientFactory.CreateClient("MyService");

        // Returns an HttpResult<User>
        var result = await client.GetAsync<User>($"users/{userId}", cancellationToken);

        if (result.IsSuccess)
        {
            return result.Value.Name;
        }

        // Handle failure — result.Errors contains messages from the server's error body
        // if it returned a structured HttpResult, or a generic fallback message otherwise.
        return null;
    }
}
```

When using the default client, call `CreateClient()` without arguments:

```csharp
var client = apiClientFactory.CreateClient();
```

### 3. POST/PUT with JSON

```csharp
var newUser = new User { Name = "John Doe" };
var result = await client.PostAsync<User, User>("users", newUser);

if (result.IsSuccess)
{
    // Created successfully
}
```

### 4. GET with Query Parameters

```csharp
var query = new { Search = "Frédéric", Page = 1 };
var result = await client.GetAsync<List<User>, object>("users", query);
```

### 5. Authentication

Pass an authentication scheme when registering a client to enable bearer token injection:

```csharp
builder.Services.AddApiClient("MyService", options =>
{
    options.BaseAddress = "https://api.example.com/v1/";
}, "Bearer");
```

This registers `IAccessTokenProvider` as a singleton service. Inject it wherever you obtain a token and store it for the client:

```csharp
using MisterMoret.Http.Authentication;

public class AuthService
{
    private readonly IAccessTokenProvider _tokenProvider;

    public AuthService(IAccessTokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    public void StoreToken(string token)
    {
        // For a specific named client
        _tokenProvider.SetAccessToken("MyService", token);

        // Or globally, for clients registered without a name
        _tokenProvider.SetAccessToken(token);
    }
}
```

The `AuthenticationHandler` automatically reads the token and attaches it as an `Authorization` header on every outgoing request for that client.

#### Built-in `AccessTokenProvider` — intended use cases

The built-in `AccessTokenProvider` stores **one token per named client** (plus one global token for the default client) for the lifetime of the application. This makes it suitable for:

- **Machine-to-machine authentication** — a single shared token for all requests (e.g. client credentials OAuth flow, API key)
- **Desktop / mobile apps** (e.g. MAUI) — one logged-in user for the entire app session
- **Background services / workers** — a service account token set once at startup or refreshed by a background task

#### Per-user scenarios (MVC, Blazor) — use a custom `IAccessTokenProvider`

The built-in implementation is **not suitable** for web apps where each user has their own token. Because it is a singleton with a single token per client, tokens from concurrent requests would overwrite each other, causing users to send each other's tokens.

For per-user scenarios, implement `IAccessTokenProvider` to read the token from the current HTTP context and register it **before** calling `AddApiClient` (so `TryAddSingleton` skips the built-in one):

```csharp
// In MVC — reads the access token stored in the authentication cookie
public class UserAccessTokenProvider : IAccessTokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetAccessToken() =>
        _httpContextAccessor.HttpContext?
            .GetTokenAsync("access_token")
            .GetAwaiter().GetResult();

    public string? GetAccessToken(string clientName) => GetAccessToken();

    public void SetAccessToken(string accessToken) { }
    public void SetAccessToken(string clientName, string accessToken) { }
}
```

```csharp
// Register before AddApiClient so TryAddSingleton skips the built-in implementation
builder.Services.AddSingleton<IAccessTokenProvider, UserAccessTokenProvider>();
builder.Services.AddApiClient("MyService", options =>
{
    options.BaseAddress = "https://api.example.com/v1/";
}, "Bearer");
```

> [!NOTE]
> In **Blazor Server**, `IHttpContextAccessor` is unreliable inside components after the initial HTTP handshake (the connection switches to SignalR). Capture the token during the initial request and store it in a scoped service instead.
>
> In **Blazor WebAssembly**, there is no server-side HTTP context. Tokens are managed in the browser and typically obtained via `IAccessTokenProvider` from `Microsoft.AspNetCore.Components.WebAssembly.Authentication`, which you can wrap.

## ⚖️ License

This project is licensed under the **MIT License** - see the [LICENSE](../../LICENSE) file for details.

## 👤 Author

**Frédéric Goetinck-Moret**
- [NuGet Profile](https://www.nuget.org/profiles/fgoetinck)
- [GitHub Profile](https://github.com/fgoetinck)
