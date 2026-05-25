namespace MisterMoret.Results.Interfaces;

/// <summary>
/// Represents the outcome of an operation that produces a value of type <typeparamref name="T"/>,
/// extending <see cref="IResult"/> with a <see cref="Value"/> property that is populated on success.
/// </summary>
/// <typeparam name="T">The type of the value produced by the operation when it succeeds.</typeparam>
public interface IResult<T> : IResult
{
    /// <summary>
    /// Gets the value produced by the operation.
    /// This property is meaningful only when <see cref="IResult.IsSuccess"/> is <see langword="true"/>;
    /// its value is <see langword="null"/> or default when the result represents a failure.
    /// </summary>
    T? Value { get; }
}