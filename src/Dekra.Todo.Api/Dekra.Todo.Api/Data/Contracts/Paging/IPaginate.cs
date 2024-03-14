namespace Dekra.Todo.Api.Data.Contracts.Paging
{
    public interface IPaginate<TResult>
    {
        int Size { get; }
        int Page { get; }
        int Total { get; }
        IList<TResult> Items { get; }
    }
}
