# MisterMoret.Http

![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)
[![Version](https://img.shields.io/badge/version-1.0.0--beta.1-orange.svg)](https://www.nuget.org/packages/MisterMoret.Http)
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
- **Result Pattern Integration**: Returns `HttpResult<T>` instead of throwing exceptions for non-success status codes.
- **Query Parameter Support**: Simplified way to pass query parameters via anonymous objects or classes.
- **Dependency Injection Ready**: Seamlessly integrates with `IServiceCollection`.
- **Modern .NET Support**: Targets **.NET 8.0, 9.0, and 10.0**.

## 🚀 Installation

Install the package via the NuGet CLI:

```bash
dotnet add package MisterMoret.Http --version 1.0.0-beta.1
```

## 💡 Usage

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

## ⚖️ License

This project is licensed under the **MIT License** - see the [LICENSE](../../LICENSE) file for details.

## 👤 Author

**Frédéric Goetinck-Moret**
- [NuGet Profile](https://www.nuget.org/profiles/fgoetinck)
- [GitHub Profile](https://github.com/fgoetinck)
