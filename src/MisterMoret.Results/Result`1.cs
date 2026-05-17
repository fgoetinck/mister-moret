using System.Collections.Generic;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

public class Result<T> : Result, IResult<T>
{
    public T Value { get; set; }

    public static Result<T> Success(T data)
    {
        return new Result<T> { Value = data };
    }

    public new static Result<T> Failure(string error)
    {
        return new Result<T> { Errors = new List<string> { error } };
    }

    public new static Result<T> Failure(List<string> errors)
    {
        return new Result<T> { Errors = errors };
    }
}