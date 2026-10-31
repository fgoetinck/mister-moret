# Changelog — MisterMoret.Http

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

## [1.0.0-beta.12] - 2026-10-31

### Changed
- `ApiClient` no longer throws `HttpRequestException` or a timeout (`TaskCanceledException`) for transport failures; it returns a failed result instead. An `HttpRequestException` yields its `StatusCode` when set, otherwise 503 (Service Unavailable) for connection, name-resolution, premature-response-end and unknown errors and 502 (Bad Gateway) for every other `HttpRequestError` (e.g. TLS/certificate errors, invalid responses), with the message "Failed to reach the server." only (the exception message is not included, so no host, IP or port leaks). An `HttpClient` timeout yields 408 (Request Timeout) with the message "The request timed out.". Cancellation requested through the caller's `CancellationToken` still throws `OperationCanceledException`, and other exceptions still propagate. Callers that caught `HttpRequestException` / `TaskCanceledException` must now check `IsSuccess` / `StatusCode`. Code that wraps `ApiClient` calls in `MisterMoret.Try`'s `TryHttpOperation` now gets the `ApiClient` status codes (and failed results) instead of the operation's exception mapper being applied to transport failures.

## [1.0.0-beta.11] - 2026-10-31

### Fixed
- `ApiClient` no longer throws on empty, malformed or non-JSON (e.g. `text/html`) response bodies, or on a JSON content type with an unsupported charset. A non-success response returns a failed `HttpResult<T>` with the status code and the fallback error message. A success response whose body cannot be deserialized returns a failure with status code 500 and the message "Failed to deserialize data." (plus the JSON error message when there is one), never a 2xx code.
- `ApiClient` now surfaces the `errors` of ASP.NET Core `ValidationProblemDetails` responses (an object of string arrays) in addition to the array form.

## [1.0.0-beta.10] - 2026-06-03

### Added
- `PostAsync<TResponse>(string endpoint, HttpContent content, CancellationToken)` overload on `IApiClient` / `ApiClient` for posting arbitrary HTTP content (e.g. `MultipartFormDataContent` for file uploads).
- `PostAsync<TRequest>(string endpoint, TRequest request, CancellationToken)` overload on `IApiClient` / `ApiClient` — POST with a JSON body when no response body is expected, returning `HttpResult`.
- `PutAsync<TRequest>(string endpoint, TRequest request, CancellationToken)` overload on `IApiClient` / `ApiClient` — PUT with a JSON body when no response body is expected, returning `HttpResult`.
- `PatchAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken)` overload on `IApiClient` / `ApiClient` — PATCH with a JSON body and a typed response, returning `HttpResult<TResponse>`.
- `PatchAsync<TRequest>(string endpoint, TRequest request, CancellationToken)` overload on `IApiClient` / `ApiClient` — PATCH with a JSON body when no response body is expected, returning `HttpResult`.

## [1.0.0-beta.9] - 2026-06-03

### Fixed
- `AccessTokenProvider` now uses `ConcurrentDictionary` instead of `Dictionary` for named token storage, making concurrent reads and writes from multiple threads safe.
- The global default token field is now `volatile`, ensuring all threads always observe the latest written value.

### Changed
- Package README updated to document that the built-in `AccessTokenProvider` is designed for global/machine tokens (client credentials, API keys, desktop/mobile apps) and is not suitable for per-user web scenarios. A custom `IAccessTokenProvider` pattern is now documented for MVC and Blazor.

## [1.0.0-beta.8] - 2026-06-03

### Fixed
- `IAccessTokenProvider` is now registered as a singleton instead of scoped. The previous scoped registration caused `AuthenticationHandler` to resolve a different instance than the one callers inject and call `SetAccessToken` on, because `IHttpClientFactory` creates its own isolated scope when building the handler pipeline — resulting in the bearer token never being injected into outgoing requests.

## [1.0.0-beta.7] - 2026-05-25

### Added
- All `IApiClient` / `ApiClient` methods now accept an optional `CancellationToken cancellationToken = default` as their last parameter: `GetAsync<TResponse>`, `GetAsync<TResponse, TQuery>`, `PostAsync<TRequest, TResponse>`, `PutAsync<TRequest, TResponse>`, `DeleteAsync<TResponse>`, and `DeleteAsync`.

## [1.0.0-beta.6] - 2026-05-24

### Fixed
- `ApiClient` no longer misreports non-success responses as successful. The previous approach deserialized error response bodies directly as `HttpResult<TResponse>`, but `protected init` setters prevented the JSON deserializer from populating `Errors`, causing every deserialized failure to appear as a success with no value. Error bodies are now deserialized into an intermediate `ApiErrorResponse` record, and the extracted errors are used to construct a correct `HttpResult<TResponse>.Failure`.

## [1.0.0-beta.5] - 2026-05-24

### Fixed
- `ApiClient` now attempts to deserialize non-success response bodies as `HttpResult<TResponse>` before falling back to a generic failure. This allows servers that return a structured `HttpResult` error payload to propagate their error messages directly to the caller.

## [1.0.0-beta.4] - 2026-05-21

### Fixed
- XML documentation file is now included in the NuGet package, enabling IntelliSense for consumers.

## [1.0.0-beta.3] - 2026-05-21

### Added
- `ApiClientOptions` class to configure `BaseAddress`, `Timeout` (default 100 s), and `UserAgent`.
- `AddApiClient` now accepts `Action<ApiClientOptions>` instead of a plain base address string.
- Unnamed/default client overload: `AddApiClient(Action<ApiClientOptions>, string?)` — accessible via `CreateClient()`.
- `ApiClientNames.Default` constant (`"default"`) for referencing the default client by name.
- `IAccessTokenProvider` and `AccessTokenProvider` for scoped, in-memory token storage (per-client and global).
- `AuthenticationHandler` (`DelegatingHandler`) for automatic bearer token injection on outgoing requests.
- Optional `authenticationScheme` parameter on both `AddApiClient` overloads to enable authentication.
- XML documentation comments on all public interfaces and classes.

### Changed
- `ApiClient` is now `sealed`.
- Interfaces moved from `MisterMoret.Http.Interfaces` to `MisterMoret.Http`.

## [1.0.0-beta.2] - 2026-05-17

### Fixed
- Updated package README to correctly reflect the available API.

## [1.0.0-beta.1] - 2026-05-17

### Added
- `IApiClient` with `GetAsync<T>`, `GetAsync<T, TQuery>`, `PostAsync`, `PutAsync`, `DeleteAsync<T>`, and `DeleteAsync`.
- `IApiClientFactory` with `CreateClient(string name)`.
- `AddApiClient(string name, string baseAddress)` extension method on `IServiceCollection`.
- Built-in JSON (de)serialization with case-insensitive property matching.
- Query parameter support via object reflection.
- All HTTP methods return `HttpResult<T>` — no exceptions thrown for non-success status codes.