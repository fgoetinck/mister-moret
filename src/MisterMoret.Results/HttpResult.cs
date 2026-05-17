using System.Collections.Generic;
using System.Net;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

public class HttpResult : IResult
{
    public HttpStatusCode Code { get; set; }
    public bool IsSuccess => Errors?.Count == 0;
    public IReadOnlyList<string> Errors { get; protected init; } = new List<string>();

    public static HttpResult Success(HttpStatusCode code = HttpStatusCode.OK)
    {
        return new HttpResult { Code = code };
    }

    public static HttpResult Failure(List<string> errors, HttpStatusCode code)
    {
        return new HttpResult { Errors = errors, Code = code };
    }

    public static HttpResult Failure(string error, HttpStatusCode code)
    {
        return new HttpResult { Errors = new List<string> { error }, Code = code };
    }
}