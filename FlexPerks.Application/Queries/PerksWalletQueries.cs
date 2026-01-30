using FlexPerks.Domain.Models;
using System.Linq.Expressions;

namespace FlexPerks.Application.Queries
{
    public static class PerksWalletQueries
    {
        public static Expression<Func<PerksWallet, bool>> ListByUserId(int userId)
            => w => w.UserId == userId;

        public static Expression<Func<PerksWallet, bool>> GetByUserAndCategory(int userId, int categoryId)
            => w => w.UserId == userId && w.CategoryId == categoryId;
    }
}
