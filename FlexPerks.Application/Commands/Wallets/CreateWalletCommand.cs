using Flunt.Notifications;
using Flunt.Validations;

namespace FlexPerks.Application.Commands.Wallets
{
    public class CreateWalletCommand : Notifiable<Notification>
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal InitialBalance { get; set; } = 0m;

        public void Validate()
        {
            AddNotifications(new Contract<CreateWalletCommand>()
            .IsGreaterThan(UserId, 0, nameof(UserId), "Usuário inválido")
            .IsGreaterThan(CategoryId, 0, nameof(CategoryId), "Categoria inválida")
            .IsGreaterOrEqualsThan(InitialBalance, 0, nameof(InitialBalance), "Saldo inicial não pode ser negativo"));
        }
    }
}