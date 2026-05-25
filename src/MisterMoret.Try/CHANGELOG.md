# Changelog — MisterMoret.Try

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added
- `TryOperation.Execute<T>(Func<Result<T>>)` — synchronous counterpart to `ExecuteAsync<T>(Func<Task<Result<T>>>)`; catches any exception and returns a failed `Result<T>` carrying the exception message.
- `TryOperation.Execute(Func<Result>)` — synchronous counterpart to `ExecuteAsync(Func<Task<Result>>)`; catches any exception and returns a failed `Result` carrying the exception message.
- `TryOperation.Execute<T>(Func<T>)` — synchronous counterpart to `ExecuteAsync<T>(Func<Task<T>>)`; auto-wraps the return value in `Result<T>.Success`.
- `TryOperation.Execute(Action)` — synchronous counterpart to `ExecuteAsync(Func<Task>)`; auto-wraps a void operation in `Result.Success`.

## [1.0.0-beta.2] - 2026-05-23

### Changed
- **Breaking**: `TryOperations` has been split into two sealed classes: `TryOperation` and `TryHttpOperation`.
- **Breaking**: All methods previously named `TryOperationAsync` are now `TryOperation.ExecuteAsync`.
- **Breaking**: All methods previously named `TryHttpOperationAsync` are now `TryHttpOperation.ExecuteAsync`.
- **Breaking**: Replace `using static MisterMoret.Try.TryOperations;` with `using MisterMoret.Try;`.

## [1.0.0-beta.1] - 2026-05-23

### Added
- `TryOperationAsync<T>(Func<Task<T>>)` — auto-wraps the return value in `Result<T>.Success`.
- `TryOperationAsync(Func<Task>)` — auto-wraps a void operation in `Result.Success`.
- `TryOperationAsync<T>(Func<Task<Result<T>>>)` — catches any exception and returns a failed `Result<T>` carrying the exception message.
- `TryOperationAsync(Func<Task<Result>>)` — catches any exception and returns a failed `Result` carrying the exception message.
- `TryHttpOperationAsync<T>(Func<Task<T>>)` — auto-wraps the return value in `HttpResult<T>.Success` with `200 OK`, mapping HTTP-related exceptions to appropriate status codes.
- `TryHttpOperationAsync(Func<Task>)` — auto-wraps a void operation in `HttpResult.Success` with `200 OK`, mapping HTTP-related exceptions to appropriate status codes.
- `TryHttpOperationAsync<T>(Func<Task<HttpResult<T>>>)` — catches HTTP-related exceptions and maps them to appropriate `HttpStatusCode` values on the returned `HttpResult<T>`.
- `TryHttpOperationAsync(Func<Task<HttpResult>>)` — catches HTTP-related exceptions and maps them to appropriate `HttpStatusCode` values on the returned `HttpResult`.