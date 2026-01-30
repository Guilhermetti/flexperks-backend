namespace FlexPerks.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken ct = default);
        Task BeginTransactionAsync(CancellationToken ct = default);
        Task CommitTransactionAsync(CancellationToken ct = default);
        Task RollbackTransactionAsync(CancellationToken ct = default);
    }
}
