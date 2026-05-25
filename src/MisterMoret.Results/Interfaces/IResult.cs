using System.Collections.Generic;

namespace MisterMoret.Results.Interfaces;

/// <summary>
/// Represents the outcome of an operation that does not produce a value, exposing
/// whether the operation succeeded and any error messages collected on failure.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether the operation completed without errors.
    /// </summary>
    /// <value><see langword="true"/> when <see cref="Errors"/> is empty; <see langword="false"/> when one or more errors are present.</value>
    bool IsSuccess { get; }

    /// <summary>
    /// Gets the error messages that describe why the operation failed.
    /// The collection is empty when <see cref="IsSuccess"/> is <see langword="true"/> and contains at least one entry when the result represents a failure.
    /// </summary>
    IReadOnlyList<string>? Errors { get; }
}