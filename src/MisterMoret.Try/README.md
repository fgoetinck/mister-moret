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
- **Auto-Wrapping**: Pass a plain `Func<Task<T>>` and let the library wrap the return value in a result for you.
- **Zero Boilerplate**: Import the namespace once and call methods directly via the class name.
- **Modern .NET Support**: Targets **.NET 8.0, 9.0, and 10.0**.

## 🚀 Installation

Install the package via the NuGet CLI:

```bash
dotnet add package MisterMoret.Try --version 1.0.0-beta.2
```

## 💡 Usage

Add a `using` directive to bring the classes into scope:

```csharp
using MisterMoret.Try;
```

### `TryOperation.ExecuteAsync` — Result Overloads

Use when you want a `Result` or `Result<T>` back.

**Auto-wrapping** — pass a plain delegate and the result is created for you:

```csharp
// Returns Result<User>
var result = await TryOperation.ExecuteAsync(() => _repository.GetUserAsync(id));

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

**Manual result** — use when you need to return a failure or perform validation inside the delegate:

```csharp
var result = await TryOperation.ExecuteAsync(async () =>
{
    var user = await _repository.GetUserAsync(id);
    if (user == null)
        return Result<User>.Failure("User not found.");
    return Result<User>.Success(user);
});
```

### `TryHttpOperation.ExecuteAsync` — HttpResult Overloads

Use when you want an `HttpResult` or `HttpResult<T>` back. HTTP-related exceptions are mapped to appropriate status codes automatically.

**Auto-wrapping** — success defaults to `200 OK`:

```csharp
// Returns HttpResult<User>
var result = await TryHttpOperation.ExecuteAsync(() => _repository.GetUserAsync(id));

if (result.IsSuccess)
{
    Console.WriteLine(result.Value.Name);
}
else if (result.Code == HttpStatusCode.RequestTimeout)
{
    Console.WriteLine("The request timed out.");
}
```

**Manual result** — use when you need control over the success status code:

```csharp
var result = await TryHttpOperation.ExecuteAsync(() => _apiClient.GetAsync<User>("users/1"));
```

#### Exception Mapping

Applies to all `TryHttpOperation.ExecuteAsync` overloads:

| Exception | Status Code |
|---|---|
| `TaskCanceledException` (timeout) | `408 Request Timeout` |
| `HttpRequestException` (with embedded code) | Embedded `HttpStatusCode` |
| `HttpRequestException` (no embedded code) | `503 Service Unavailable` |
| Any other `Exception` | `500 Internal Server Error` |

## ⚖️ License

This project is licensed under the **MIT License** - see the [LICENSE](../../LICENSE) file for details.

## 👤 Author

**Frédéric Goetinck-Moret**
- [NuGet Profile](https://www.nuget.org/profiles/fgoetinck)
- [GitHub Profile](https://github.com/fgoetinck)