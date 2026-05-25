using System.Collections.Generic;
using System.Net;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

/// <summary>
/// Represents the outcome of an HTTP operation that does not produce a value,
/// pairing a success/failure indicator and optional error messages with an <see cref="HttpStatusCode"/>.
/// Use the <see cref="Success(HttpStatusCode)"/> and <see cref="Failure(string, HttpStatusCode)"/> factory methods to construct instances.
/// For operations that also carry a response value, use <see cref="HttpResult{T}"/> instead.
/// </summary>
public class HttpResult : IResult
{
    /// <summary>
    /// Gets or sets the HTTP status code associated with this result.
    /// Set by the factory methods to reflect the status code returned by the HTTP operation.
    /// </summary>
    public HttpStatusCode Code { get; set; }

    /// <summary>
    /// Gets a value indicating whether the result represents a successful outcome.
    /// </summary>
    /// <value><see langword="true"/> when <see cref="Errors"/> is empty; <see langword="false"/> when one or more errors are present.</value>
    public bool IsSuccess => Errors?.Count == 0;

    /// <summary>
    /// Gets the error messages that describe why the HTTP operation failed.
    /// The collection is empty when the result was created by <see cref="Success(HttpStatusCode)"/> and contains at least one entry
    /// when the result was created by a <see cref="Failure(string, HttpStatusCode)"/> or <see cref="Failure(List{string}, HttpStatusCode)"/> overload.
    /// </summary>
    public IReadOnlyList<string>? Errors { get; protected init; } = new List<string>();

    /// <summary>
    /// Creates a successful result with the specified HTTP status code.
    /// </summary>
    /// <param name="code">The HTTP status code to associate with the result. Defaults to <see cref="HttpStatusCode.OK"/>.</param>
    /// <returns>An <see cref="HttpResult"/> whose <see cref="IsSuccess"/> property is <see langword="true"/> and whose <see cref="Code"/> is <paramref name="code"/>.</returns>
    public static HttpResult Success(HttpStatusCode code = HttpStatusCode.OK)
    {
        return new HttpResult { Code = code };
    }

    /// <summary>
    /// Creates a failed result containing the supplied error messages and the specified HTTP status code.
    /// </summary>
    /// <param name="errors">The error messages that describe the failure. Cannot be <see langword="null"/>.</param>
    /// <param name="code">The HTTP status code that reflects the nature of the failure (for example, <see cref="HttpStatusCode.BadRequest"/> or <see cref="HttpStatusCode.InternalServerError"/>).</param>
    /// <returns>An <see cref="HttpResult"/> whose <see cref="IsSuccess"/> property is <see langword="false"/>, whose <see cref="Errors"/> contains <paramref name="errors"/>, and whose <see cref="Code"/> is <paramref name="code"/>.</returns>
    public static HttpResult Failure(List<string> errors, HttpStatusCode code)
    {
        return new HttpResult { Errors = errors, Code = code };
    }

    /// <summary>
    /// Creates a failed result containing a single error message and the specified HTTP status code.
    /// </summary>
    /// <param name="error">The error message that describes the failure. Cannot be <see langword="null"/>.</param>
    /// <param name="code">The HTTP status code that reflects the nature of the failure (for example, <see cref="HttpStatusCode.BadRequest"/> or <see cref="HttpStatusCode.InternalServerError"/>).</param>
    /// <returns>An <see cref="HttpResult"/> whose <see cref="IsSuccess"/> property is <see langword="false"/>, whose <see cref="Errors"/> contains <paramref name="error"/>, and whose <see cref="Code"/> is <paramref name="code"/>.</returns>
    public static HttpResult Failure(string error, HttpStatusCode code)
    {
        return new HttpResult { Errors = new List<string> { error }, Code = code };
    }
}