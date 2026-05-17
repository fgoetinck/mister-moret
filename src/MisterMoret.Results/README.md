# MisterMoret.Results

![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)
![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512bd4.svg?logo=dotnet)
![.NET 9.0](https://img.shields.io/badge/.NET-9.0-512bd4.svg?logo=dotnet)
![.NET 10.0](https://img.shields.io/badge/.NET-10.0-512bd4.svg?logo=dotnet)

A lightweight implementation of the Result pattern for .NET applications.

> [!NOTE]  
> This package is currently in beta and available via GitHub Packages for testing. It may be published to NuGet.org in the future.

## Features

- **Generic `Result<T>`**: Wrap operation outcomes with data.
- **`HttpResult`**: Specialized result for HTTP operations including status codes.
- **Success/Failure states**: Cleanly handle flow control without exceptions.
- **Error Collection**: Easily collect and report multiple errors.

## Installation

To use this package, you need to configure a `nuget.config` file in your project's root directory. See the [Root README](../../README.md#installation) for detailed instructions on how to set up the GitHub Packages registry.

Once configured, you can install the package via NuGet:

```bash
dotnet add package MisterMoret.Results --version 0.1.0-beta
```

## Usage

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

Useful for API responses where the HTTP status code matters.

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

## License
This project is licensed under the MIT License - see the [LICENSE](../../LICENSE) file for details.
