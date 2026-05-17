# MisterMoret

A collection of useful .NET packages for building robust applications. This repository currently contains two main packages focused on operation results and HTTP communication.

> [!NOTE]  
> These packages are currently in beta and available via GitHub Packages for testing. They may be published to NuGet.org in the future.

## Packages

### 1. MisterMoret.Results
A lightweight implementation of the Result pattern to handle operation outcomes without relying on exceptions for flow control.

#### Features
- `Result` & `Result<T>`: Standard success/failure wrapper.
- `HttpResult` & `HttpResult<T>`: Specialized result types that include `HttpStatusCode`, perfect for Web APIs.
- Clean and expressive API for creating results.
- Supports **.NET 8.0, 9.0, and 10.0**.

#### Usage Example
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
A generic API client built on top of `HttpClient` that integrates seamlessly with `MisterMoret.Results`.

#### Features
- Generic `IApiClient` for GET, POST, PUT, and DELETE operations.
- `IApiClientFactory` for creating named clients using `IHttpClientFactory`.
- Automatically handles JSON (de)serialization with case-insensitive property matching.
- Returns `HttpResult<T>` or `HttpResult` for easy error handling and status code checking.
- Built-in support for query parameters via anonymous objects or classes.
- Easy Dependency Injection setup.
- Supports **.NET 8.0, 9.0, and 10.0**.

#### Usage Example

**Registration:**
```csharp
// Registers IApiClientFactory and configures a named HttpClient
builder.Services.AddApiClient("MyApi", "https://api.example.com");
```

**Using the Factory:**
```csharp
public class MyService
{
    private readonly IApiClient _apiClient;

    public MyService(IApiClientFactory apiClientFactory)
    {
        // Create a client by name
        _apiClient = apiClientFactory.CreateClient("MyApi");
    }

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

## Installation

To use these packages, you need to configure a `nuget.config` file in your project's root directory. 

### Option 1: Create a new `nuget.config`
If you don't have one, create a file named `nuget.config` with the following content:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
    <add key="MisterMoret" value="https://nuget.pkg.github.com/fgoetinck/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <MisterMoret>
      <add key="Username" value="YOUR_USERNAME" />
      <add key="ClearTextPassword" value="YOUR_GITHUB_TOKEN" />
    </MisterMoret>
  </packageSourceCredentials>
</configuration>
```

### Option 2: Update an existing `nuget.config`
If you already have a `nuget.config`, add the following entries to their respective sections:

**`packageSources`:**
```xml
<add key="MisterMoret" value="https://nuget.pkg.github.com/fgoetinck/index.json" />
```

**`packageSourceCredentials`:**
```xml
<MisterMoret>
  <add key="Username" value="YOUR_USERNAME" />
  <add key="ClearTextPassword" value="YOUR_GITHUB_TOKEN" />
</MisterMoret>
```

---

You can then install the packages via NuGet:

```bash
dotnet add package MisterMoret.Results --version 0.1.0-beta
dotnet add package MisterMoret.Http --version 0.1.0-beta
```

## Future Plans
These packages are currently being beta tested in real-world projects via GitHub Packages. They will be made publicly available on NuGet.org once they reach a stable release.


