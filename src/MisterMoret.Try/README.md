# MisterMoret.Try

![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)
[![NuGet](https://img.shields.io/nuget/vpre/MisterMoret.Try.svg)](https://www.nuget.org/packages/MisterMoret.Try)
![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512bd4.svg?logo=dotnet)
![.NET 9.0](https://img.shields.io/badge/.NET-9.0-512bd4.svg?logo=dotnet)
![.NET 10.0](https://img.shields.io/badge/.NET-10.0-512bd4.svg?logo=dotnet)
[![NuGet](https://img.shields.io/badge/NuGet-fgoetinck-blue.svg?logo=nuget)](https://www.nuget.org/profiles/fgoetinck)
[![GitHub](https://img.shields.io/badge/GitHub-fgoetinck-181717.svg?logo=github)](https://github.com/fgoetinck)

A lightweight try/catch wrapper that converts unhandled exceptions into failed `Result` or `HttpResult` objects, eliminating boilerplate exception handling in .NET applications.

> [!NOTE]
> This package is currently in **beta** and available via [NuGet.org](https://www.nuget.org/).

## ✨ Features

- **Exception-Safe Execution**: Wrap any async delegate and receive a failed result instead of a thrown exception.
- **Result Integration**: Works seamlessly with `MisterMoret.Results` — returns `Result<T>`, `Result`, `HttpResult<T>`, and `HttpResult`.
- **HTTP-Aware**: Maps HTTP-related exceptions to meaningful `HttpStatusCode` values automatically.
- **Zero Boilerplate**: Use `using static` to call methods without any class prefix.
- **Modern .NET Support**: Targets **.NET 8.0, 9.0, and 10.0**.

## 🚀 Installation

Install the package via the NuGet CLI:

```bash
dotnet add package MisterMoret.Try
```

## 💡 Usage

Add a `using static` directive to call the methods directly without a class prefix:

```csharp
using static MisterMoret.Try.TryOperations;
```

### Result Overloads

Catches any exception and returns it as a failed `Result` or `Result<T>`:

```csharp
var result = await TryOperationAsync(() => _repository.GetUserAsync(id));

if (result.IsSuccess)
{
    Console.WriteLine(result.Value.Name);
}
else
{
    foreach (var error in result.Errors)
    {
        Console.WriteLine($"Error: {error}");
    }
}
```

### HttpResult Overloads

Maps HTTP-related exceptions to appropriate status codes:

```csharp
var result = await TryOperationAsync(() => _apiClient.GetAsync<User>("users/1"));

if (result.IsSuccess)
{
    Console.WriteLine(result.Value.Name);
}
else if (result.Code == HttpStatusCode.RequestTimeout)
{
    Console.WriteLine("The request timed out.");
}
```

#### Exception Mapping

| Exception | Status Code |
|---|---|
| `TaskCanceledException` (timeout) | `408 Request Timeout` |
| `HttpRequestException` (with embedded code) | Embedded `HttpStatusCode` |
| `HttpRequestException` (no embedded code) | `503 Service Unavailable` |
| Any other `Exception` | `500 Internal Server Error` |

## ⚖️ License

This project is licensed under the **MIT License** - see the [LICENSE](../LICENSE) file for details.

## 👤 Author

**Frédéric Goetinck-Moret**
- [NuGet Profile](https://www.nuget.org/profiles/fgoetinck)
- [GitHub Profile](https://github.com/fgoetinck)