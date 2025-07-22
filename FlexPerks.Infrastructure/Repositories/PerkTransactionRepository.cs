using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using FlexPerks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPerks.Infrastructure.Repositories
{
    public class PerkTransactionRepository : GenericRepository<PerkTransaction>, IPerkTransactionRepository
    {
        public PerkTransactionRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<PerkTransaction>> ListByWalletId(int walletId)
        {
            return await _dbContext.PerkTransactions
                .Where(t => t.WalletId == walletId)
                .ToListAsync();
        }
    }
}
