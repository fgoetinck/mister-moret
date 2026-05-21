using System.Collections.Generic;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

public class Result<T> : Result, IResult<T>
{
    /// <summary>
    /// Gets or sets the value carried by this result.
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Creates a successful result carrying the specified value.
    /// </summary>
    /// <param name="data">The value to carry in the result.</param>
    /// <returns>A successful <see cref="Result{T}"/> containing <paramref name="data"/>.</returns>
    public static Result<T> Success(T data)
    {
        return new Result<T> { Value = data };
    }

    /// <summary>
    /// Creates a failed result with a single error message.
    /// </summary>
    /// <param name="error">The error message describing the failure.</param>
    /// <returns>A failed <see cref="Result{T}"/> containing <paramref name="error"/>.</returns>
    public new static Result<T> Failure(string error)
    {
        return new Result<T> { Errors = new List<string> { error } };
    }

    /// <summary>
    /// Creates a failed result with the specified list of error messages.
    /// </summary>
    /// <param name="errors">The list of error messages describing the failure.</param>
    /// <returns>A failed <see cref="Result{T}"/> containing <paramref name="errors"/>.</returns>
    public new static Result<T> Failure(List<string> errors)
    {
        return new Result<T> { Errors = errors };
    }
}