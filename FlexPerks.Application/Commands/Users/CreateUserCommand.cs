using Flunt.Notifications;
using Flunt.Validations;

namespace FlexPerks.Application.Commands.Users
{
    public class CreateUserCommand : Notifiable<Notification>
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(new Contract<CreateUserCommand>()
            .IsGreaterThan(CompanyId, 0, nameof(CompanyId), "Empresa inválida")
            .IsNotNullOrWhiteSpace(Name, nameof(Name), "Nome é obrigatório")
            .IsGreaterOrEqualsThan(Name, 3, nameof(Name), "Nome deve ter ao menos 3 caracteres")
            .IsNotNullOrWhiteSpace(Email, nameof(Email), "Email é obrigatório")
            .IsEmail(Email, nameof(Email), "Email inválido")
            .IsNotNullOrWhiteSpace(Password, nameof(Password), "Senha é obrigatória")
            .IsGreaterOrEqualsThan(Password, 6, nameof(Password), "Senha deve ter ao menos 6 caracteres"));
        }
    }
}
