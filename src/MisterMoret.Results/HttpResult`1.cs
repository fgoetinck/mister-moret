using System.Collections.Generic;
using System.Net;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

public class HttpResult<T> : HttpResult, IResult<T>
{
    /// <summary>
    /// Gets or sets the value carried by this result.
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Creates a successful result carrying the specified value and HTTP status code.
    /// </summary>
    /// <param name="data">The value to carry in the result.</param>
    /// <param name="code">The HTTP status code to associate with the result. Defaults to <see cref="HttpStatusCode.OK"/>.</param>
    /// <returns>A successful <see cref="HttpResult{T}"/> containing <paramref name="data"/> and <paramref name="code"/>.</returns>
    public static HttpResult<T> Success(T data, HttpStatusCode code = HttpStatusCode.OK)
    {
        return new HttpResult<T> { Value = data, Code = code };
    }

    /// <summary>
    /// Creates a failed result with a single error message and HTTP status code.
    /// </summary>
    /// <param name="error">The error message describing the failure.</param>
    /// <param name="code">The HTTP status code to associate with the failure.</param>
    /// <returns>A failed <see cref="HttpResult{T}"/> containing <paramref name="error"/> and <paramref name="code"/>.</returns>
    public new static HttpResult<T> Failure(string error, HttpStatusCode code)
    {
        return new HttpResult<T> { Errors = new List<string> { error }, Code = code };
    }

    /// <summary>
    /// Creates a failed result with the specified list of error messages and HTTP status code.
    /// </summary>
    /// <param name="errors">The list of error messages describing the failure.</param>
    /// <param name="code">The HTTP status code to associate with the failure.</param>
    /// <returns>A failed <see cref="HttpResult{T}"/> containing <paramref name="errors"/> and <paramref name="code"/>.</returns>
    public new static HttpResult<T> Failure(List<string> errors, HttpStatusCode code)
    {
        return new HttpResult<T> { Errors = errors, Code = code };
    }
}