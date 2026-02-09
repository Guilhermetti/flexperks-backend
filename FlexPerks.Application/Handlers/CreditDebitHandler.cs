using FlexPerks.Application.Commands.Transactions;
using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using Flunt.Notifications;

namespace FlexPerks.Application.Handlers
{
    public class CreditDebitHandler : Notifiable<Notification>
    {
        private readonly IPerksWalletRepository _wallets;
        private readonly IPerkTransactionRepository _tx;
        private readonly IUnitOfWork _uow;

        public CreditDebitHandler(
            IPerksWalletRepository wallets,
            IPerkTransactionRepository tx,
            IUnitOfWork uow)
        {
            _wallets = wallets;
            _tx = tx;
            _uow = uow;
        }

        public async Task<int?> Handle(CreditDebitCommand cmd)
        {
            Clear();
            cmd.Validate();
            if (!cmd.IsValid)
            {
                AddNotifications(cmd.Notifications);
                return null;
            }

            var wallet = await _wallets.GetById(cmd.WalletId);
            if (wallet is null)
            {
                AddNotification("WalletId", "Carteira não encontrada");
                return null;
            }

            if (cmd.Type == "Debit")
            {
                if (wallet.Balance < cmd.Amount)
                {
                    AddNotification("Amount", "Saldo insuficiente");
                    return null;
                }

                wallet.Balance -= cmd.Amount;
            }
            else
            {
                wallet.Balance += cmd.Amount;
            }

            await _wallets.Update(wallet);

            var tx = new PerkTransaction { WalletId = wallet.Id, Amount = cmd.Amount, Type = cmd.Type };
            await _tx.Insert(tx);
            await _uow.CommitAsync();
            return tx.Id;
        }
    }
}
