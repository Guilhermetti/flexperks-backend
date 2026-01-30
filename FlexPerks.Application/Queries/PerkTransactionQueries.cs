using FlexPerks.Domain.Models;
using System.Linq.Expressions;

namespace FlexPerks.Application.Queries
{
    public static class PerkTransactionQueries
    {
        public static Expression<Func<PerkTransaction, bool>> ByWalletId(int walletId)
            => t => t.WalletId == walletId;
    }
}
