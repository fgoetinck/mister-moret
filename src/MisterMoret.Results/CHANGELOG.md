# Changelog — MisterMoret.Results

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

## [1.0.0-beta.6] - 2026-11-22

### Changed
- The list-based `Failure` overloads of `Result`, `Result<T>`, `HttpResult` and `HttpResult<T>` now take `IEnumerable<string>` instead of `List<string>`, so another result's `Errors` (or an array) can be passed without `.ToList()`. This is binary breaking (recompile against this version) and source compatible for `List<string>` callers. `Failure` now copies the errors, so later changes to the caller's collection no longer affect the result.

### Fixed
- `Failure` with an empty error collection no longer creates a successful result; it throws `ArgumentException`. A `null` collection or a `null` single error throws `ArgumentNullException`. A result created by `Failure` always has `IsSuccess == false`.

## [1.0.0-beta.5] - 2026-05-25

### Changed
- Improved XML documentation across all public types: added missing class-level summaries on `IResult`, `IResult<T>`, `Result`, `Result<T>`, `HttpResult`, and `HttpResult<T>`; added `<value>` tags on properties; cross-referenced related members by their qualified names for accurate IntelliSense navigation.

## [1.0.0-beta.4] - 2026-05-21

### Fixed
- XML documentation file is now included in the NuGet package, enabling IntelliSense for consumers.

## [1.0.0-beta.3] - 2026-05-21

### Added
- XML documentation comments on all public interfaces and classes.
- Nullable reference type annotations on `IResult` and `IResult<T>`.

## [1.0.0-beta.2] - 2026-05-17

### Fixed
- Updated package README to correctly reflect the available API.

## [1.0.0-beta.1] - 2026-05-17

### Added
- `IResult` and `IResult<T>` interfaces exposing `IsSuccess`, `Errors`, and `Value`.
- `Result` and `Result<T>` — standard success/failure wrappers with `Success(...)` and `Failure(...)` factory methods.
- `HttpResult` and `HttpResult<T>` — HTTP-specific variants carrying an `HttpStatusCode`.