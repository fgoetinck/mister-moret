using System.Collections.Generic;
using MisterMoret.Results.Interfaces;

namespace MisterMoret.Results;

public class Result : IResult
{
    public bool IsSuccess => Errors?.Count == 0;
    public IReadOnlyList<string> Errors { get; protected init; } = new List<string>();

    public static Result Success()
        => new Result();

    public static Result Failure(List<string> errors)
        => new Result() { Errors = errors };

    public static Result Failure(string error)
        => new Result() { Errors = new List<string> { error } };
}