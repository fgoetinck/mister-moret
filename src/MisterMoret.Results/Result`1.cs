using System.Collections.Generic;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

/// <summary>
/// Represents the outcome of an operation that produces a value of type <typeparamref name="T"/>.
/// Use the <see cref="Success(T)"/> and <see cref="Failure(string)"/> factory methods to construct instances.
/// For operations that also carry an HTTP status code, use <see cref="HttpResult{T}"/> instead.
/// </summary>
/// <typeparam name="T">The type of the value produced by the operation when it succeeds.</typeparam>
public class Result<T> : Result, IResult<T>
{
    /// <summary>
    /// Gets the value produced by the operation.
    /// Populated only when the result was created by <see cref="Success(T)"/>;
    /// <see langword="null"/> or default when the result represents a failure.
    /// </summary>
    public T? Value { get; protected init; }

    /// <summary>
    /// Creates a successful result carrying the specified value.
    /// </summary>
    /// <param name="data">The value produced by the operation. May be <see langword="null"/> for nullable <typeparamref name="T"/>.</param>
    /// <returns>A <see cref="Result{T}"/> whose <see cref="Result.IsSuccess"/> property is <see langword="true"/> and whose <see cref="Value"/> is <paramref name="data"/>.</returns>
    public static Result<T> Success(T data)
    {
        return new Result<T> { Value = data };
    }

    /// <summary>
    /// Creates a failed result containing a single error message.
    /// </summary>
    /// <param name="error">The error message that describes the failure. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="Result{T}"/> whose <see cref="Result.IsSuccess"/> property is <see langword="false"/> and whose <see cref="Result.Errors"/> contains <paramref name="error"/>.</returns>
    public new static Result<T> Failure(string error)
    {
        return new Result<T> { Errors = new List<string> { error } };
    }

    /// <summary>
    /// Creates a failed result containing the supplied error messages.
    /// </summary>
    /// <param name="errors">The error messages that describe the failure. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="Result{T}"/> whose <see cref="Result.IsSuccess"/> property is <see langword="false"/> and whose <see cref="Result.Errors"/> contains <paramref name="errors"/>.</returns>
    public new static Result<T> Failure(List<string> errors)
    {
        return new Result<T> { Errors = errors };
    }
}