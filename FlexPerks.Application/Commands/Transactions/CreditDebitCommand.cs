using Flunt.Notifications;
using Flunt.Validations;

namespace FlexPerks.Application.Commands.Transactions
{
    public class CreditDebitCommand : Notifiable<Notification>
    {
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = "Credit"; // "Credit" ou "Debit"

        public void Validate()
        {
            AddNotifications(new Contract<CreditDebitCommand>()
            .IsGreaterThan(WalletId, 0, nameof(WalletId), "Carteira inválida")
            .IsGreaterThan(Amount, 0, nameof(Amount), "Valor deve ser > 0")
            .IsTrue(Type == "Credit" || Type == "Debit", nameof(Type), "Tipo inválido"));
        }
    }
}
