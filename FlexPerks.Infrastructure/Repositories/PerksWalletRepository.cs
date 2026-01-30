using FlexPerks.Application.Interfaces;
using FlexPerks.Application.Queries;
using FlexPerks.Domain.Models;
using FlexPerks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPerks.Infrastructure.Repositories
{
    public class PerksWalletRepository : GenericRepository<PerksWallet>, IPerksWalletRepository
    {
        public PerksWalletRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<PerksWallet>> ListByUserId(int userId)
        {
            return await _dbContext.PerksWallets
                .Where(PerksWalletQueries.ListByUserId(userId))
                .ToListAsync();
        }

        public async Task<PerksWallet?> GetByUserAndCategory(int userId, int categoryId)
        {
            return await _dbContext.PerksWallets
                .FirstOrDefaultAsync(PerksWalletQueries.GetByUserAndCategory(userId, categoryId));
        }
    }
}
