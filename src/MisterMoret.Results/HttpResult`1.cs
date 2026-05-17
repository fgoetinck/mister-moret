using System.Collections.Generic;
using System.Net;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

public class HttpResult<T> : HttpResult, IResult<T>
{
    public T Data { get; set; }

    public static HttpResult<T> Success(T data, HttpStatusCode code = HttpStatusCode.OK)
    {
        return new HttpResult<T> { Data = data, Code = code };
    }

    public new static HttpResult<T> Failure(string error, HttpStatusCode code)
    {
        return new HttpResult<T> { Errors = new List<string> { error }, Code = code };
    }

    public new static HttpResult<T> Failure(List<string> errors, HttpStatusCode code)
    {
        return new HttpResult<T> { Errors = errors, Code = code };
    }
}