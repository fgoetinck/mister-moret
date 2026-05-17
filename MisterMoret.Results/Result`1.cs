using System.Collections.Generic;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results
{
    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }

        public static HttpResult<T> Success(T data)
            => new HttpResult<T>() { Data = data };

        public new static HttpResult<T> Failure(string error)
            => new HttpResult<T>() { Errors = new List<string> { error } };

        public new static HttpResult<T> Failure(List<string> errors)
            => new HttpResult<T>() { Errors = errors };
    }
}