# MisterMoret

A collection of useful .NET packages for building robust applications. This repository currently contains two main packages focused on operation results and HTTP communication.

> [!NOTE]  
> These projects are currently being developed as packages and will be published in the future.

## Packages

### 1. MisterMoret.Results
A lightweight implementation of the Result pattern to handle operation outcomes without relying on exceptions for flow control.

#### Features
- `Result` & `Result<T>`: Standard success/failure wrapper.
- `HttpResult` & `HttpResult<T>`: Specialized result types that include `HttpStatusCode`, perfect for Web APIs.
- Clean and expressive API for creating results.

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
- Automatically handles JSON (de)serialization.
- Returns `HttpResult<T>` for easy error handling and status code checking.
- Built-in support for query parameters via anonymous objects or classes.
- Easy Dependency Injection setup.

#### Usage Example

**Registration:**
```csharp
builder.Services.AddApiClient("https://api.example.com");
```

**Using the Client:**
```csharp
public class MyService
{
    private readonly IApiClient _apiClient;

    public MyService(IApiClient apiClient)
    {
        _apiClient = apiClient;
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

## Future Plans
I am currently working on finalizing these packages in a private GitHub repository for testing in real-world projects before making them publicly available as NuGet packages.


