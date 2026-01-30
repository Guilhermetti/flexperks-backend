using FlexPerks.Application.Commands.Wallets;
using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using Flunt.Notifications;

namespace FlexPerks.Application.Handlers.Wallets
{
    public class CreateWalletHandler : Notifiable<Notification>
    {
        private readonly IPerksWalletRepository _wallets;
        private readonly IUnitOfWork _uow;

        public CreateWalletHandler(
            IPerksWalletRepository wallets,
            IUnitOfWork uow)
        {
            _wallets = wallets;
            _uow = uow;
        }

        public async Task<int?> Handle(CreateWalletCommand cmd)
        {
            cmd.Validate();
            if (!cmd.IsValid)
            {
                AddNotifications(cmd.Notifications);
                return null;
            }

            var exists = await _wallets.GetByUserAndCategory(cmd.UserId, cmd.CategoryId);
            if (exists != null)
            {
                AddNotification("Wallet", "Carteira já existe para o usuário nesta categoria");
                return null;
            }

            var wallet = new PerksWallet { UserId = cmd.UserId, CategoryId = cmd.CategoryId, Balance = cmd.InitialBalance };
            await _wallets.Insert(wallet);
            await _uow.CommitAsync();
            return wallet.Id;
        }
    }
}
