using System.Collections.Generic;

namespace MisterMoret.Results.Interfaces
{
    public interface IResult
    {
        bool IsSuccess { get; }
        List<string> Errors { get; set; }
    }
}