using System.Collections.Generic;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

/// <summary>
/// Represents the outcome of an operation that does not produce a value.
/// Use the <see cref="Success()"/> and <see cref="Failure(string)"/> factory methods to construct instances.
/// Subclass this type to carry additional outcome data; for operations that return an HTTP status code,
/// use <see cref="HttpResult"/> instead.
/// </summary>
public class Result : IResult
{
    /// <summary>
    /// Gets a value indicating whether the result represents a successful outcome.
    /// </summary>
    /// <value><see langword="true"/> when <see cref="Errors"/> is empty; <see langword="false"/> when one or more errors are present.</value>
    public bool IsSuccess => Errors?.Count == 0;

    /// <summary>
    /// Gets the error messages that describe why the operation failed.
    /// The collection is empty when the result was created by <see cref="Success()"/> and contains at least one entry
    /// when the result was created by a <see cref="Failure(string)"/> or <see cref="Failure(List{string})"/> overload.
    /// </summary>
    public IReadOnlyList<string>? Errors { get; protected init; } = new List<string>();

    /// <summary>
    /// Creates a successful result with an empty error list.
    /// </summary>
    /// <returns>A <see cref="Result"/> whose <see cref="IsSuccess"/> property is <see langword="true"/>.</returns>
    public static Result Success()
    {
        return new Result();
    }

    /// <summary>
    /// Creates a failed result containing the supplied error messages.
    /// </summary>
    /// <param name="errors">The error messages that describe the failure. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="Result"/> whose <see cref="IsSuccess"/> property is <see langword="false"/> and whose <see cref="Errors"/> contains <paramref name="errors"/>.</returns>
    public static Result Failure(List<string> errors)
    {
        return new Result { Errors = errors };
    }

    /// <summary>
    /// Creates a failed result containing a single error message.
    /// </summary>
    /// <param name="error">The error message that describes the failure. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="Result"/> whose <see cref="IsSuccess"/> property is <see langword="false"/> and whose <see cref="Errors"/> contains <paramref name="error"/>.</returns>
    public static Result Failure(string error)
    {
        return new Result { Errors = new List<string> { error } };
    }
}