using System.Collections.Generic;
using System.Net;

namespace MisterMoret.Results
{
    public class HttpResult
    {
        public bool IsSuccess => Errors?.Count == 0;
        public List<string> Errors { get; set; } = new List<string>();
        public HttpStatusCode Code { get; set; }

        public static HttpResult Success(HttpStatusCode code = HttpStatusCode.OK)
            => new HttpResult() { Code = code };

        public static HttpResult Failure(List<string> errors, HttpStatusCode code)
            => new HttpResult() { Errors = errors, Code = code };

        public static HttpResult Failure(string error, HttpStatusCode code)
            => new HttpResult() { Errors = new List<string> { error }, Code = code };
    }
}