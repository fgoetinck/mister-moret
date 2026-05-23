# Changelog — MisterMoret.Try

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added
- `TryOperationAsync<T>(Func<Task<Result<T>>>)` — catches any exception and returns a failed `Result<T>` carrying the exception message.
- `TryOperationAsync(Func<Task<Result>>)` — catches any exception and returns a failed `Result` carrying the exception message.
- `TryOperationAsync<T>(Func<Task<HttpResult<T>>>)` — catches HTTP-related exceptions and maps them to appropriate `HttpStatusCode` values on the returned `HttpResult<T>`.
- `TryOperationAsync(Func<Task<HttpResult>>)` — catches HTTP-related exceptions and maps them to appropriate `HttpStatusCode` values on the returned `HttpResult`.