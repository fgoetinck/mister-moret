# Changelog — MisterMoret.Results

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

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