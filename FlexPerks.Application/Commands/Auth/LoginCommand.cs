using Flunt.Notifications;
using Flunt.Validations;

namespace FlexPerks.Application.Commands.Auth
{

    public class LoginCommand : Notifiable<Notification>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(new Contract<LoginCommand>()
                .IsNotNullOrWhiteSpace(Email, nameof(Email), "Email é obrigatório")
                .IsEmail(Email, nameof(Email), "Email inválido")
                .IsNotNullOrWhiteSpace(Password, nameof(Password), "Senha é obrigatória")
                .IsGreaterOrEqualsThan(Password, 6, nameof(Password), "Senha deve ter ao menos 6 caracteres"));
        }
    }
}
