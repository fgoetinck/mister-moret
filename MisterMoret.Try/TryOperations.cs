using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MisterMoret.Results;

namespace MisterMoret.Try;

public static class TryOperations
{
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