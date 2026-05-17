# MisterMoret.Http

A simple and extensible API client wrapper for .NET, built on top of `IHttpClientFactory` and integrated with `MisterMoret.Results`.

> [!NOTE]  
> This package is currently in beta and available via GitHub Packages for testing. It may be published to NuGet.org in the future.

## Features

- **Named API Clients**: Easily register and use multiple API clients.
- **Typed Responses**: Automatic JSON deserialization into typed objects.
- **Result Pattern Integration**: Returns `HttpResult<T>` instead of throwing exceptions for non-success status codes.
- **Query Parameter Support**: Simplified way to pass query parameters.
- **Dependency Injection Ready**: Seamlessly integrates with `IServiceCollection`.

## Installation

To use these packages, you need to configure a `nuget.config` file in your project's root directory. See the [Root README](../../README.md#installation) for detailed instructions on how to set up the GitHub Packages registry.

Once configured, you can install the package via NuGet:

```bash
dotnet add package MisterMoret.Http --version 0.1.0-beta
```

## Usage

### 1. Registration

Register your API client in `Program.cs` or `Startup.cs`:

```csharp
using MisterMoret.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register a named client with a base address
builder.Services.AddApiClient("MyService", "https://api.example.com/v1/");
```

### 2. Basic Usage

Inject `IApiClientFactory` and create a client by name:

```csharp
using MisterMoret.Http.Interfaces;

public class MyService(IApiClientFactory apiClientFactory)
{
    public async Task<string?> GetUserName(int userId)
    {
        var client = apiClientFactory.CreateClient("MyService");
        
        // Returns an HttpResult<User>
        var result = await client.GetAsync<User>($"users/{userId}");
        
        if (result.IsSuccess)
        {
            return result.Value.Name;
        }
        
        // Handle failure (e.g., result.Code, result.Errors)
        return null;
    }
}
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
