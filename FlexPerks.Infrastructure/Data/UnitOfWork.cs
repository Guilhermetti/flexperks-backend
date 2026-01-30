using System.Threading;
using System.Threading.Tasks;
using FlexPerks.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace FlexPerks.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private IDbContextTransaction _tx;

        public UnitOfWork(ApplicationDbContext db) => _db = db;

        public async Task BeginTransactionAsync(CancellationToken ct = default)
        {
            if (_tx == null)
                _tx = await _db.Database.BeginTransactionAsync(ct);
        }

        public async Task<int> CommitAsync(CancellationToken ct = default)
        {
            var rows = await _db.SaveChangesAsync(ct);
            if (_tx != null)
            {
                await _tx.CommitAsync(ct);
                await _tx.DisposeAsync();
                _tx = null;
            }
            return rows;
        }

        public async Task CommitTransactionAsync(CancellationToken ct = default)
        {
            if (_tx != null)
            {
                await _tx.CommitAsync(ct);
                await _tx.DisposeAsync();
                _tx = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken ct = default)
        {
            if (_tx != null)
            {
                await _tx.RollbackAsync(ct);
                await _tx.DisposeAsync();
                _tx = null;
            }
        }
    }
}
