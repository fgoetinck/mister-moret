using System.Collections.Generic;

namespace MisterMoret.Results.Interfaces;

public interface IResult
{
    bool IsSuccess { get; }
    IReadOnlyList<string> Errors { get; }
}