# MisterMoret.Results

A lightweight implementation of the Result pattern for .NET applications.

## Features

- **Generic `Result<T>`**: Wrap operation outcomes with data.
- **`HttpResult`**: Specialized result for HTTP operations including status codes.
- **Success/Failure states**: Cleanly handle flow control without exceptions.
- **Error Collection**: Easily collect and report multiple errors.

## Installation

You can install the package via NuGet:

```bash
dotnet add package MisterMoret.Results --source https://nuget.pkg.github.com/fgoetinck/index.json
```

*(Note: Since this is a private package, ensure your `NuGet.Config` is configured to access the GitHub Packages registry.)*

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
