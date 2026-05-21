using System.Collections.Generic;

namespace MisterMoret.Results.Interfaces;

public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether the operation completed without errors.
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Gets the list of error messages describing why the operation failed. Empty when <see cref="IsSuccess"/> is <see langword="true"/>.
    /// </summary>
    IReadOnlyList<string>? Errors { get; }
}