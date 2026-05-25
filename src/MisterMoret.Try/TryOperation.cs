using System;
using System.Threading.Tasks;
using MisterMoret.Results;

namespace MisterMoret.Try;

/// <summary>
/// Provides static helper methods that execute synchronous and asynchronous operations
/// inside a structured try/catch boundary and translate any thrown exception into a failed
/// <see cref="Result"/>, preventing unhandled exceptions from escaping the call site.
/// </summary>
public static class TryOperation
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
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// The <see cref="Result{T}"/> produced by <paramref name="operation"/> when it
    /// completes without throwing, or a failed <see cref="Result{T}"/> whose error message
    /// is produced by <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<Result<T>> ExecuteAsync<T>(Func<Task<Result<T>>> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return await operation();
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return Result<T>.Failure(message);
        }
    }

    /// <summary>
    /// Executes a synchronous operation that returns a <see cref="Result{T}"/> and
    /// catches any unhandled exception, returning it as a failed result instead of
    /// allowing it to propagate.
    /// </summary>
    /// <typeparam name="T">The type of the value carried by a successful result.</typeparam>
    /// <param name="operation">
    /// The synchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// The <see cref="Result{T}"/> produced by <paramref name="operation"/> when it
    /// completes without throwing, or a failed <see cref="Result{T}"/> whose error message
    /// is produced by <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static Result<T> Execute<T>(Func<Result<T>> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return operation();
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return Result<T>.Failure(message);
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
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// The <see cref="Result"/> produced by <paramref name="operation"/> when it
    /// completes without throwing, or a failed <see cref="Result"/> whose error message
    /// is produced by <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<Result> ExecuteAsync(Func<Task<Result>> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return await operation();
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return Result.Failure(message);
        }
    }

    /// <summary>
    /// Executes a synchronous operation that returns a <see cref="Result"/> and
    /// catches any unhandled exception, returning it as a failed result instead of
    /// allowing it to propagate.
    /// </summary>
    /// <param name="operation">
    /// The synchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// The <see cref="Result"/> produced by <paramref name="operation"/> when it
    /// completes without throwing, or a failed <see cref="Result"/> whose error message
    /// is produced by <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static Result Execute(Func<Result> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return operation();
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return Result.Failure(message);
        }
    }

    /// <summary>
    /// Executes an asynchronous operation that produces a value of type <typeparamref name="T"/>
    /// and wraps a successful outcome in a <see cref="Result{T}"/>, catching any unhandled
    /// exception and returning it as a failed result instead of allowing it to propagate.
    /// </summary>
    /// <typeparam name="T">The type of the value produced by the operation.</typeparam>
    /// <param name="operation">
    /// The asynchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// A successful <see cref="Result{T}"/> carrying the value returned by
    /// <paramref name="operation"/>, or a failed <see cref="Result{T}"/> whose error message
    /// is produced by <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<Result<T>> ExecuteAsync<T>(Func<Task<T>> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return Result<T>.Success(await operation());
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return Result<T>.Failure(message);
        }
    }

    /// <summary>
    /// Executes a synchronous operation that produces a value of type <typeparamref name="T"/>
    /// and wraps a successful outcome in a <see cref="Result{T}"/>, catching any unhandled
    /// exception and returning it as a failed result instead of allowing it to propagate.
    /// </summary>
    /// <typeparam name="T">The type of the value produced by the operation.</typeparam>
    /// <param name="operation">
    /// The synchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// A successful <see cref="Result{T}"/> carrying the value returned by
    /// <paramref name="operation"/>, or a failed <see cref="Result{T}"/> whose error message
    /// is produced by <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static Result<T> Execute<T>(Func<T> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            return Result<T>.Success(operation());
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return Result<T>.Failure(message);
        }
    }

    /// <summary>
    /// Executes an asynchronous operation that produces no value and wraps a successful
    /// outcome in a <see cref="Result"/>, catching any unhandled exception and returning
    /// it as a failed result instead of allowing it to propagate.
    /// </summary>
    /// <param name="operation">
    /// The asynchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// A successful <see cref="Result"/> when <paramref name="operation"/> completes without
    /// throwing, or a failed <see cref="Result"/> whose error message is produced by
    /// <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static async Task<Result> ExecuteAsync(Func<Task> operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            await operation();
            return Result.Success();
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return Result.Failure(message);
        }
    }

    /// <summary>
    /// Executes a synchronous operation that produces no value and wraps a successful
    /// outcome in a <see cref="Result"/>, catching any unhandled exception and returning
    /// it as a failed result instead of allowing it to propagate.
    /// </summary>
    /// <param name="operation">
    /// The synchronous delegate to invoke. Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="exceptionMapper">
    /// An optional delegate that maps a caught <see cref="Exception"/> to a custom error message.
    /// When <see langword="null"/>, <see cref="Exception.Message"/> is used instead.
    /// </param>
    /// <returns>
    /// A successful <see cref="Result"/> when <paramref name="operation"/> completes without
    /// throwing, or a failed <see cref="Result"/> whose error message is produced by
    /// <paramref name="exceptionMapper"/> when supplied, falling back to
    /// <see cref="Exception.Message"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="operation"/> is <see langword="null"/>.
    /// </exception>
    public static Result Execute(Action operation,
        Func<Exception, string>? exceptionMapper = null)
    {
        ArgumentNullException.ThrowIfNull(operation);
        try
        {
            operation();
            return Result.Success();
        }
        catch (Exception e)
        {
            var message = exceptionMapper?.Invoke(e) ?? e.Message;
            return Result.Failure(message);
        }
    }
}