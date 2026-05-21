namespace MisterMoret.Results.Interfaces;

public interface IResult<T> : IResult
{
    /// <summary>
    /// Gets the value produced by the operation. Only meaningful when <see cref="IResult.IsSuccess"/> is <see langword="true"/>.
    /// </summary>
    T? Value { get; }
}