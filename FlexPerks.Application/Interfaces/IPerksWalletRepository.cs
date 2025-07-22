using FlexPerks.Domain.Models;

namespace FlexPerks.Application.Interfaces
{
    public interface IPerksWalletRepository : IAsyncRepository<PerksWallet>
    {
        Task<IEnumerable<PerksWallet>> ListByUserId(int userId);
    }
}
