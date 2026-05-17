namespace MisterMoret.Results.Interfaces;

public interface IResult<T> : IResult
{
    T Value { get; set; }
}