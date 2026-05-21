using System.Collections.Generic;
using System.Net;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

public class HttpResult : IResult
{
    /// <summary>
    /// Gets or sets the HTTP status code associated with this result.
    /// </summary>
    public HttpStatusCode Code { get; set; }

    /// <summary>
    /// Gets a value indicating whether the result represents a successful outcome.
    /// </summary>
    public bool IsSuccess => Errors?.Count == 0;

    /// <summary>
    /// Gets the list of error messages associated with this result.
    /// </summary>
    public IReadOnlyList<string> Errors { get; protected init; } = new List<string>();

    /// <summary>
    /// Creates a successful result with the specified HTTP status code.
    /// </summary>
    /// <param name="code">The HTTP status code to associate with the result. Defaults to <see cref="HttpStatusCode.OK"/>.</param>
    /// <returns>A successful <see cref="HttpResult"/> with the given <paramref name="code"/>.</returns>
    public static HttpResult Success(HttpStatusCode code = HttpStatusCode.OK)
    {
        return new HttpResult { Code = code };
    }

    /// <summary>
    /// Creates a failed result with the specified list of error messages and HTTP status code.
    /// </summary>
    /// <param name="errors">The list of error messages describing the failure.</param>
    /// <param name="code">The HTTP status code to associate with the failure.</param>
    /// <returns>A failed <see cref="HttpResult"/> containing <paramref name="errors"/> and <paramref name="code"/>.</returns>
    public static HttpResult Failure(List<string> errors, HttpStatusCode code)
    {
        return new HttpResult { Errors = errors, Code = code };
    }

    /// <summary>
    /// Creates a failed result with a single error message and HTTP status code.
    /// </summary>
    /// <param name="error">The error message describing the failure.</param>
    /// <param name="code">The HTTP status code to associate with the failure.</param>
    /// <returns>A failed <see cref="HttpResult"/> containing <paramref name="error"/> and <paramref name="code"/>.</returns>
    public static HttpResult Failure(string error, HttpStatusCode code)
    {
        return new HttpResult { Errors = new List<string> { error }, Code = code };
    }
}