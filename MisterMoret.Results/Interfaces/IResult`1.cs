namespace MisterMoret.Results.Interfaces
{
    public interface IResult<T> : IResult
    {
        T Data { get; set; }
    }
}