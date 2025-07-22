using FlexPerks.Domain.Models;

namespace FlexPerks.Application.Interfaces
{
    public interface IPerkTransactionRepository : IAsyncRepository<PerkTransaction>
    {
        Task<IEnumerable<PerkTransaction>> ListByWalletId(int walletId);
    }
}
