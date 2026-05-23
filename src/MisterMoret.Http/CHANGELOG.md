# Changelog — MisterMoret.Http

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

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