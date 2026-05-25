using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MisterMoret.Results;

namespace MisterMoret.Try;

/// <summary>
/// Provides static helper methods that execute asynchronous HTTP operations inside a structured
/// try/catch boundary and map common HTTP-related exceptions to semantically appropriate
/// <see cref="HttpStatusCode"/> values on the returned <see cref="HttpResult"/>,
/// preventing unhandled exceptions from escaping the call site.
/// </summary>
public static class TryHttpOperation
{
    /// <summary>
    /// Executes an asynchronous HTTP operation that returns an <see cref="HttpResult{T}"/>
    /// and maps common HTTP-related exceptions to semantically appropriate HTTP status codes
    /// on the returned failure result.
    /// </summary>
    /// <remarks>
    /// Exception mapping applied when <paramref name="operation"/> throws:
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       <see cref="TaskCanceledException"/> whose <see cref="Exception.InnerException"/>
    ///       is a <see cref="TimeoutException"/> — mapped to
    ///       <see cref="HttpStatusCode.RequestTimeout"/> (408).
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <see cref="HttpRequestException"/> — mapped to the status code embedded in
    ///       <see cref="HttpRequestException.StatusCode"/>, or
    ///       <see cref="HttpStatusCode.ServiceUnavailable"/> (503) when that property is
    ///       <see langword="null"/>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Any other <see cref="Exception"/> — mapped to
    ///       <see cref="HttpStatusCode.InternalServerError"/> (500).
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    /// <typeparam name="T">The type of the value carried by a successful result.</typeparam>
    /// <param name="operation">
    /// The asynchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// The <see cref="HttpResult{T}"/> produced by <paramref name="operation"/> when it
    /// completes without throwing, or a failed <see cref="HttpResult{T}"/> with an HTTP status
    /// code derived from the caught exception and an error message produced by
    /// <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<HttpResult<T>> ExecuteAsync<T>(Func<Task<HttpResult<T>>> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return await operation();
        }
        catch (TaskCanceledException e) when (e.InnerException is TimeoutException)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult<T>.Failure(message, HttpStatusCode.RequestTimeout);
        }
        catch (HttpRequestException e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult<T>.Failure(message, e.StatusCode ?? HttpStatusCode.ServiceUnavailable);
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult<T>.Failure(message, HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Executes an asynchronous HTTP operation that returns an <see cref="HttpResult"/>
    /// and maps common HTTP-related exceptions to semantically appropriate HTTP status codes
    /// on the returned failure result.
    /// </summary>
    /// <remarks>
    /// Exception mapping applied when <paramref name="operation"/> throws:
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       <see cref="TaskCanceledException"/> whose <see cref="Exception.InnerException"/>
    ///       is a <see cref="TimeoutException"/> — mapped to
    ///       <see cref="HttpStatusCode.RequestTimeout"/> (408).
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <see cref="HttpRequestException"/> — mapped to the status code embedded in
    ///       <see cref="HttpRequestException.StatusCode"/>, or
    ///       <see cref="HttpStatusCode.ServiceUnavailable"/> (503) when that property is
    ///       <see langword="null"/>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Any other <see cref="Exception"/> — mapped to
    ///       <see cref="HttpStatusCode.InternalServerError"/> (500).
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    /// <param name="operation">
    /// The asynchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// The <see cref="HttpResult"/> produced by <paramref name="operation"/> when it
    /// completes without throwing, or a failed <see cref="HttpResult"/> with an HTTP status
    /// code derived from the caught exception and an error message produced by
    /// <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<HttpResult> ExecuteAsync(Func<Task<HttpResult>> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return await operation();
        }
        catch (TaskCanceledException e) when (e.InnerException is TimeoutException)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult.Failure(message, HttpStatusCode.RequestTimeout);
        }
        catch (HttpRequestException e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult.Failure(message, e.StatusCode ?? HttpStatusCode.ServiceUnavailable);
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult.Failure(message, HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Executes an asynchronous operation that produces a value of type <typeparamref name="T"/>
    /// and wraps a successful outcome in an <see cref="HttpResult{T}"/> with
    /// <see cref="HttpStatusCode.OK"/> (200), catching common HTTP-related exceptions and
    /// mapping them to semantically appropriate status codes on the returned failure result.
    /// </summary>
    /// <remarks>
    /// Exception mapping applied when <paramref name="operation"/> throws:
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       <see cref="TaskCanceledException"/> whose <see cref="Exception.InnerException"/>
    ///       is a <see cref="TimeoutException"/> — mapped to
    ///       <see cref="HttpStatusCode.RequestTimeout"/> (408).
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <see cref="HttpRequestException"/> — mapped to the status code embedded in
    ///       <see cref="HttpRequestException.StatusCode"/>, or
    ///       <see cref="HttpStatusCode.ServiceUnavailable"/> (503) when that property is
    ///       <see langword="null"/>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Any other <see cref="Exception"/> — mapped to
    ///       <see cref="HttpStatusCode.InternalServerError"/> (500).
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    /// <typeparam name="T">The type of the value produced by the operation.</typeparam>
    /// <param name="operation">
    /// The asynchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// A successful <see cref="HttpResult{T}"/> with <see cref="HttpStatusCode.OK"/> carrying
    /// the value returned by <paramref name="operation"/>, or a failed <see cref="HttpResult{T}"/>
    /// with an HTTP status code derived from the caught exception and an error message produced by
    /// <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<HttpResult<T>> ExecuteAsync<T>(Func<Task<T>> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return HttpResult<T>.Success(await operation());
        }
        catch (TaskCanceledException e) when (e.InnerException is TimeoutException)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult<T>.Failure(message, HttpStatusCode.RequestTimeout);
        }
        catch (HttpRequestException e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult<T>.Failure(message, e.StatusCode ?? HttpStatusCode.ServiceUnavailable);
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult<T>.Failure(message, HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Executes an asynchronous operation that produces no value and wraps a successful
    /// outcome in an <see cref="HttpResult"/> with <see cref="HttpStatusCode.OK"/> (200),
    /// catching common HTTP-related exceptions and mapping them to semantically appropriate
    /// status codes on the returned failure result.
    /// </summary>
    /// <remarks>
    /// Exception mapping applied when <paramref name="operation"/> throws:
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       <see cref="TaskCanceledException"/> whose <see cref="Exception.InnerException"/>
    ///       is a <see cref="TimeoutException"/> — mapped to
    ///       <see cref="HttpStatusCode.RequestTimeout"/> (408).
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <see cref="HttpRequestException"/> — mapped to the status code embedded in
    ///       <see cref="HttpRequestException.StatusCode"/>, or
    ///       <see cref="HttpStatusCode.ServiceUnavailable"/> (503) when that property is
    ///       <see langword="null"/>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Any other <see cref="Exception"/> — mapped to
    ///       <see cref="HttpStatusCode.InternalServerError"/> (500).
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    /// <param name="operation">
    /// The asynchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// A successful <see cref="HttpResult"/> with <see cref="HttpStatusCode.OK"/> when
    /// <paramref name="operation"/> completes without throwing, or a failed <see cref="HttpResult"/>
    /// with an HTTP status code derived from the caught exception and an error message produced by
    /// <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<HttpResult> ExecuteAsync(Func<Task> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            await operation();
            return HttpResult.Success();
        }
        catch (TaskCanceledException e) when (e.InnerException is TimeoutException)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult.Failure(message, HttpStatusCode.RequestTimeout);
        }
        catch (HttpRequestException e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult.Failure(message, e.StatusCode ?? HttpStatusCode.ServiceUnavailable);
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return HttpResult.Failure(message, HttpStatusCode.InternalServerError);
        }
    }
}
