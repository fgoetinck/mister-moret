using System.Collections.Generic;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

public class Result : IResult
{
    /// <summary>
    /// Gets a value indicating whether the result represents a successful outcome.
    /// </summary>
    public bool IsSuccess => Errors?.Count == 0;

    /// <summary>
    /// Gets the list of error messages associated with this result.
    /// </summary>
    public IReadOnlyList<string> Errors { get; protected init; } = new List<string>();

    /// <summary>
    /// Creates a successful result with no errors.
    /// </summary>
    /// <returns>A successful <see cref="Result"/>.</returns>
    public static Result Success()
    {
        return new Result();
    }

    /// <summary>
    /// Creates a failed result with the specified list of error messages.
    /// </summary>
    /// <param name="errors">The list of error messages describing the failure.</param>
    /// <returns>A failed <see cref="Result"/> containing <paramref name="errors"/>.</returns>
    public static Result Failure(List<string> errors)
    {
        return new Result { Errors = errors };
    }

    /// <summary>
    /// Creates a failed result with a single error message.
    /// </summary>
    /// <param name="error">The error message describing the failure.</param>
    /// <returns>A failed <see cref="Result"/> containing <paramref name="error"/>.</returns>
    public static Result Failure(string error)
    {
        return new Result { Errors = new List<string> { error } };
    }
}