using System.Collections.Generic;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results
{
    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }

        public static Result<T> Success(T data)
            => new Result<T>() { Data = data };

        public new static Result<T> Failure(string error)
            => new Result<T>() { Errors = new List<string> { error } };

        public new static Result<T> Failure(List<string> errors)
            => new Result<T>() { Errors = errors };
    }
}