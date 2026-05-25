using System.Collections.Generic;
using System.Net;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

/// <summary>
/// Represents the outcome of an HTTP operation that produces a value of type <typeparamref name="T"/>,
/// pairing a success/failure indicator, optional error messages, and a response value with an <see cref="HttpStatusCode"/>.
/// Use the <see cref="Success(T, HttpStatusCode)"/> and <see cref="Failure(string, HttpStatusCode)"/> factory methods to construct instances.
/// </summary>
/// <typeparam name="T">The type of the value produced by the HTTP operation when it succeeds.</typeparam>
public class HttpResult<T> : HttpResult, IResult<T>
{
    /// <summary>
    /// Gets the value produced by the HTTP operation.
    /// Populated only when the result was created by <see cref="Success(T, HttpStatusCode)"/>;
    /// <see langword="null"/> or default when the result represents a failure.
    /// </summary>
    public T? Value { get; protected init; }

    /// <summary>
    /// Creates a successful result carrying the specified value and HTTP status code.
    /// </summary>
    /// <param name="data">The value produced by the HTTP operation. May be <see langword="null"/> for nullable <typeparamref name="T"/>.</param>
    /// <param name="code">The HTTP status code to associate with the result. Defaults to <see cref="HttpStatusCode.OK"/>.</param>
    /// <returns>An <see cref="HttpResult{T}"/> whose <see cref="HttpResult.IsSuccess"/> property is <see langword="true"/>, whose <see cref="Value"/> is <paramref name="data"/>, and whose <see cref="HttpResult.Code"/> is <paramref name="code"/>.</returns>
    public static HttpResult<T> Success(T data, HttpStatusCode code = HttpStatusCode.OK)
    {
        return new HttpResult<T> { Value = data, Code = code };
    }

    /// <summary>
    /// Creates a failed result containing a single error message and the specified HTTP status code.
    /// </summary>
    /// <param name="error">The error message that describes the failure. Cannot be <see langword="null"/>.</param>
    /// <param name="code">The HTTP status code that reflects the nature of the failure (for example, <see cref="HttpStatusCode.BadRequest"/> or <see cref="HttpStatusCode.InternalServerError"/>).</param>
    /// <returns>An <see cref="HttpResult{T}"/> whose <see cref="HttpResult.IsSuccess"/> property is <see langword="false"/>, whose <see cref="HttpResult.Errors"/> contains <paramref name="error"/>, and whose <see cref="HttpResult.Code"/> is <paramref name="code"/>.</returns>
    public new static HttpResult<T> Failure(string error, HttpStatusCode code)
    {
        return new HttpResult<T> { Errors = new List<string> { error }, Code = code };
    }

    /// <summary>
    /// Creates a failed result containing the supplied error messages and the specified HTTP status code.
    /// </summary>
    /// <param name="errors">The error messages that describe the failure. Cannot be <see langword="null"/>.</param>
    /// <param name="code">The HTTP status code that reflects the nature of the failure (for example, <see cref="HttpStatusCode.BadRequest"/> or <see cref="HttpStatusCode.InternalServerError"/>).</param>
    /// <returns>An <see cref="HttpResult{T}"/> whose <see cref="HttpResult.IsSuccess"/> property is <see langword="false"/>, whose <see cref="HttpResult.Errors"/> contains <paramref name="errors"/>, and whose <see cref="HttpResult.Code"/> is <paramref name="code"/>.</returns>
    public new static HttpResult<T> Failure(List<string> errors, HttpStatusCode code)
    {
        return new HttpResult<T> { Errors = errors, Code = code };
    }
}