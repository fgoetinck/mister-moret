# MisterMoret

![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)
![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512bd4.svg?logo=dotnet)
![.NET 9.0](https://img.shields.io/badge/.NET-9.0-512bd4.svg?logo=dotnet)
![.NET 10.0](https://img.shields.io/badge/.NET-10.0-512bd4.svg?logo=dotnet)
[![NuGet](https://img.shields.io/badge/NuGet-fgoetinck-blue.svg?logo=nuget)](https://www.nuget.org/profiles/fgoetinck)
[![GitHub](https://img.shields.io/badge/GitHub-fgoetinck-181717.svg?logo=github)](https://github.com/fgoetinck)

A collection of useful .NET packages for building robust applications. This repository currently contains three packages focused on operation results, HTTP communication, and exception-safe execution.

> [!NOTE]
> These packages are currently in **beta** and available via [NuGet.org](https://www.nuget.org/).

## 📖 Table of Contents

- [📦 Packages](#-packages)
  - [📦 MisterMoret.Results](#1-mistermoretresults)
  - [📦 MisterMoret.Http](#2-mistermorethttp)
  - [📦 MisterMoret.Try](#3-mistermorrettry)
- [🚀 Installation](#-installation)
- [📅 Future Plans](#-future-plans)
- [⚖️ License](#️-license)
- [👤 Author](#-author)

## 📦 Packages

### 1. MisterMoret.Results
[![NuGet](https://img.shields.io/nuget/vpre/MisterMoret.Results.svg)](https://www.nuget.org/packages/MisterMoret.Results)

A lightweight implementation of the **Result pattern** to handle operation outcomes without relying on exceptions for flow control.

#### ✨ Key Features
- **Success/Failure Wrapper**: `Result` & `Result<T>` for standard outcomes.
- **Web API Ready**: `HttpResult` & `HttpResult<T>` specialized for HTTP status codes.
- **Expressive API**: Clean syntax for creating and handling results.
- **Modern .NET**: Fully supports **.NET 8.0, 9.0, and 10.0**.

#### 💡 Usage Example
```csharp
public Result<User> GetUser(int id)
{
    var user = _repository.Find(id);
    if (user == null)
        return Result<User>.Failure("User not found");

    return Result<User>.Success(user);
}

// Handling the result
var result = GetUser(1);
if (result.IsSuccess)
{
    Console.WriteLine($"Hello, {result.Value.Name}");
}
else
{
    Console.WriteLine(string.Join(", ", result.Errors));
}
```

---

### 2. MisterMoret.Http
[![NuGet](https://img.shields.io/nuget/vpre/MisterMoret.Http.svg)](https://www.nuget.org/packages/MisterMoret.Http)

A generic **API client** built on top of `HttpClient` that integrates seamlessly with `MisterMoret.Results`.

#### ✨ Key Features
- **Generic Client**: `IApiClient` for standard CRUD operations.
- **Named Clients**: `IApiClientFactory` integration with `IHttpClientFactory`.
- **Auto-Serialization**: Handles JSON (de)serialization with case-insensitive property matching.
- **Result Integration**: Returns `HttpResult<T>` for robust error handling. On non-success responses, structured error bodies from the server are surfaced directly when available.
- **Query Support**: Built-in support for query parameters via objects.
- **Authentication Support**: Built-in bearer token injection via `IAccessTokenProvider`.
- **Configurable Options**: Control base address, timeout, and user-agent through `ApiClientOptions`.
- **Modern .NET**: Fully supports **.NET 8.0, 9.0, and 10.0**.

#### 💡 Usage Example

**Registration:**
```csharp
// Register a named client
builder.Services.AddApiClient("MyApi", options =>
{
    options.BaseAddress = "https://api.example.com";
});

// Optionally enable bearer token authentication
builder.Services.AddApiClient("MyApi", options =>
{
    options.BaseAddress = "https://api.example.com";
}, "Bearer");
```

**Using the Factory:**
```csharp
public class MyService(IApiClientFactory apiClientFactory)
{
    private readonly IApiClient _apiClient = apiClientFactory.CreateClient("MyApi");

    public async Task UpdateUserAsync(UserDto user)
    {
        var result = await _apiClient.PutAsync<UserDto, UserResponse>("users", user);

        if (result.IsSuccess)
        {
            // Handle success
        }
        else if (result.Code == HttpStatusCode.Unauthorized)
        {
            // Handle unauthorized
        }
    }
}
```

---

### 3. MisterMoret.Try
[![NuGet](https://img.shields.io/nuget/vpre/MisterMoret.Try.svg)](https://www.nuget.org/packages/MisterMoret.Try)

A lightweight **try/catch wrapper** that converts unhandled exceptions into failed `Result` or `HttpResult` objects, eliminating boilerplate exception handling.

#### ✨ Key Features
- **Exception-Safe Execution**: Wrap any synchronous or asynchronous delegate and receive a failed result instead of a thrown exception.
- **HTTP-Aware**: Maps `HttpRequestException` and timeouts to meaningful `HttpStatusCode` values.
- **Zero Boilerplate**: Use `using MisterMoret.Try;` and call `TryOperation.Execute(...)` / `TryOperation.ExecuteAsync(...)` or `TryHttpOperation.ExecuteAsync(...)` directly.
- **Modern .NET**: Fully supports **.NET 8.0, 9.0, and 10.0**.

#### 💡 Usage Example
```csharp
using MisterMoret.Try;

var result = await TryOperation.ExecuteAsync(() => _repository.GetUserAsync(id));

if (result.IsSuccess)
{
    Console.WriteLine(result.Value.Name);
}
else
{
    Console.WriteLine(string.Join(", ", result.Errors));
}
```

## 🚀 Installation

Install the packages via the NuGet CLI:

```bash
dotnet add package MisterMoret.Results
dotnet add package MisterMoret.Http
dotnet add package MisterMoret.Try
```

## 📅 Future Plans

These packages are currently in beta. They will be updated based on feedback and usage before reaching a stable **1.0.0** release.

## ⚖️ License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**Frédéric Goetinck-Moret**
- [NuGet Profile](https://www.nuget.org/profiles/fgoetinck)
- [GitHub Profile](https://github.com/fgoetinck)


