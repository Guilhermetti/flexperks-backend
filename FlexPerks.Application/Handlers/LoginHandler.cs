using FlexPerks.Application.Commands.Auth;
using FlexPerks.Application.Interfaces;
using FlexPerks.Application.Results.Auth;
using Flunt.Notifications;

namespace FlexPerks.Application.Handlers
{
    public class LoginHandler : Notifiable<Notification>
    {
        private readonly IUserRepository _users;
        private readonly ITokenService _tokens;

        public LoginHandler(
            IUserRepository users,
            ITokenService tokens)
        {
            _users = users;
            _tokens = tokens;
        }

        public async Task<LoginResult?> Handle(LoginCommand cmd)
        {
            Clear();
            cmd.Validate();
            if (!cmd.IsValid)
            {
                AddNotifications(cmd.Notifications);
                return null;
            }

            var user = await _users.GetByEmail(cmd.CompanyId, cmd.Email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(cmd.Password, user.PasswordHash))
            {
                AddNotification("Credentials", "Usuário ou senha inválidos");
                return null;
            }

            var token = _tokens.GenerateAccessToken(user);

            return new LoginResult
            {
                AccessToken = token,
                ExpiresAt = _tokens.GetExpiration(),
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
