# MisterMoret.Results

![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)
[![Version](https://img.shields.io/badge/version-1.0.0--beta.1-orange.svg)](https://www.nuget.org/packages/MisterMoret.Results)
![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512bd4.svg?logo=dotnet)
![.NET 9.0](https://img.shields.io/badge/.NET-9.0-512bd4.svg?logo=dotnet)
![.NET 10.0](https://img.shields.io/badge/.NET-10.0-512bd4.svg?logo=dotnet)
[![NuGet](https://img.shields.io/badge/NuGet-fgoetinck-blue.svg?logo=nuget)](https://www.nuget.org/profiles/fgoetinck)
[![GitHub](https://img.shields.io/badge/GitHub-fgoetinck-181717.svg?logo=github)](https://github.com/fgoetinck)

A lightweight implementation of the **Result pattern** for .NET applications, providing a robust way to handle operation outcomes without relying on exceptions for flow control.

> [!NOTE]
> This package is currently in **beta** and available via [NuGet.org](https://www.nuget.org/).

## ✨ Features

- **Generic `Result<T>`**: Wrap operation outcomes with data and error information.
- **Web API Specialized**: `HttpResult` includes HTTP status codes for seamless API integration.
- **Clean Flow Control**: Avoid "exception-driven development" by using success/failure states.
- **Error Collection**: Collect and report multiple errors from a single operation.
- **Modern .NET Support**: Targets **.NET 8.0, 9.0, and 10.0**.

## 🚀 Installation

Install the package via the NuGet CLI:

```bash
dotnet add package MisterMoret.Results --version 1.0.0-beta.1
```

## 💡 Usage

### Simple Result

```csharp
using MisterMoret.Results;

public Result<string> GetGreeting(string name)
{
    if (string.IsNullOrEmpty(name))
    {
        return Result<string>.Failure("Name cannot be empty.");
    }
    return Result<string>.Success($"Hello, {name}!");
}

// Consuming
var result = GetGreeting("Frédéric");
if (result.IsSuccess)
{
    Console.WriteLine(result.Value);
}
else
{
    foreach (var error in result.Errors)
    {
        Console.WriteLine($"Error: {error}");
    }
}
```

### HttpResult

Perfect for Web API controllers where the status code needs to be explicitly set.

```csharp
using System.Net;
using MisterMoret.Results;

public HttpResult UpdateUser(int id, User user)
{
    if (id <= 0)
    {
        return HttpResult.Failure("Invalid ID", HttpStatusCode.BadRequest);
    }
    
    // ... logic ...
    
    return HttpResult.Success(HttpStatusCode.NoContent);
}
```

## ⚖️ License

This project is licensed under the **MIT License** - see the [LICENSE](../../LICENSE) file for details.

## 👤 Author

**Frédéric Goetinck-Moret**
- [NuGet Profile](https://www.nuget.org/profiles/fgoetinck)
- [GitHub Profile](https://github.com/fgoetinck)
