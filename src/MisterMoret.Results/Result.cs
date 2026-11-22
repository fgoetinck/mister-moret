using System;
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
    /// when the result was created by a <see cref="Failure(string)"/> or <see cref="Failure(IEnumerable{string})"/> overload.
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
    /// <param name="errors">The error messages that describe the failure. Must contain at least one message. The messages are copied, so later changes to <paramref name="errors"/> do not affect the result.</param>
    /// <returns>A <see cref="Result"/> whose <see cref="IsSuccess"/> property is <see langword="false"/> and whose <see cref="Errors"/> contains a copy of <paramref name="errors"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="errors"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="errors"/> is empty.</exception>
    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result { Errors = CopyErrors(errors) };
    }

    /// <summary>
    /// Creates a failed result containing a single error message.
    /// </summary>
    /// <param name="error">The error message that describes the failure. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="Result"/> whose <see cref="IsSuccess"/> property is <see langword="false"/> and whose <see cref="Errors"/> contains <paramref name="error"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="error"/> is <see langword="null"/>.</exception>
    public static Result Failure(string error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return new Result { Errors = new List<string> { error } };
    }

    // IsSuccess is derived from Errors, so a failure must never end up with an empty or caller-owned list.
    internal static List<string> CopyErrors(IEnumerable<string> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        var copy = new List<string>(errors);
        if (copy.Count == 0)
            throw new ArgumentException("A failure requires at least one error message.", nameof(errors));

        return copy;
    }
}