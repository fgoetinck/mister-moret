using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MisterMoret.Results;

namespace MisterMoret.Try;

/// <summary>
/// Provides static helper methods that execute asynchronous operations inside a structured
/// try/catch boundary and translate any thrown exception into a failed <see cref="Result"/>
/// or <see cref="HttpResult"/>, preventing unhandled exceptions from escaping the call site.
/// </summary>
public static class TryOperations
{
    /// <summary>
    /// Executes an asynchronous operation that returns a <see cref="Result{T}"/> and
    /// catches any unhandled exception, returning it as a failed result instead of
    /// allowing it to propagate.
    /// </summary>
    /// <typeparam name="T">The type of the value carried by a successful result.</typeparam>
    /// <param name="operation">
    /// The asynchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <returns>
    /// The <see cref="Result{T}"/> produced by <paramref name="operation"/> when it
    /// completes without throwing, or a failed <see cref="Result{T}"/> whose error
    /// message is the <see cref="Exception.Message"/> of the caught exception.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<Result<T>> TryOperationAsync<T>(Func<Task<Result<T>>> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return await operation();
        }
        catch (Exception e)
        {
            return Result<T>.Failure(e.Message);
        }
    }

    /// <summary>
    /// Executes an asynchronous operation that returns a <see cref="Result"/> and
    /// catches any unhandled exception, returning it as a failed result instead of
    /// allowing it to propagate.
    /// </summary>
    /// <param name="operation">
    /// The asynchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <returns>
    /// The <see cref="Result"/> produced by <paramref name="operation"/> when it
    /// completes without throwing, or a failed <see cref="Result"/> whose error
    /// message is the <see cref="Exception.Message"/> of the caught exception.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<Result> TryOperationAsync(Func<Task<Result>> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return await operation();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }

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
    /// <returns>
    /// The <see cref="HttpResult{T}"/> produced by <paramref name="operation"/> when it
    /// completes without throwing, or a failed <see cref="HttpResult{T}"/> with an error
    /// message and HTTP status code derived from the caught exception.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<HttpResult<T>> TryOperationAsync<T>(Func<Task<HttpResult<T>>> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return await operation();
        }
        catch (TaskCanceledException e) when (e.InnerException is TimeoutException)
        {
            return HttpResult<T>.Failure(e.Message, HttpStatusCode.RequestTimeout);
        }
        catch (HttpRequestException e)
        {
            return HttpResult<T>.Failure(e.Message, e.StatusCode ?? HttpStatusCode.ServiceUnavailable);
        }
        catch (Exception e)
        {
            return HttpResult<T>.Failure(e.Message, HttpStatusCode.InternalServerError);
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
    /// <returns>
    /// The <see cref="HttpResult"/> produced by <paramref name="operation"/> when it
    /// completes without throwing, or a failed <see cref="HttpResult"/> with an error
    /// message and HTTP status code derived from the caught exception.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<HttpResult> TryOperationAsync(Func<Task<HttpResult>> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return await operation();
        }
        catch (TaskCanceledException e) when (e.InnerException is TimeoutException)
        {
            return HttpResult.Failure(e.Message, HttpStatusCode.RequestTimeout);
        }
        catch (HttpRequestException e)
        {
            return HttpResult.Failure(e.Message, e.StatusCode ?? HttpStatusCode.ServiceUnavailable);
        }
        catch (Exception e)
        {
            return HttpResult.Failure(e.Message, HttpStatusCode.InternalServerError);
        }
    }
}