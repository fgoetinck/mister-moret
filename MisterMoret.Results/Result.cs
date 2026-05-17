using System.Collections.Generic;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results
{
    public class Result : IResult
    {
        public bool IsSuccess => Errors?.Count == 0;
        public List<string> Errors { get; set; } = new List<string>();

        public static HttpResult Success()
            => new HttpResult();

        public static HttpResult Failure(List<string> errors)
            => new HttpResult() { Errors = errors };

        public static HttpResult Failure(string error)
            => new HttpResult() { Errors = new List<string> { error } };
    }
}