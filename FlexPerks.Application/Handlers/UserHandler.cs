using FlexPerks.Application.Commands.Users;
using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using Flunt.Notifications;

namespace FlexPerks.Application.Handlers
{
    public class UserHandler : Notifiable<Notification>
    {
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _uow;

        public UserHandler(
            IUserRepository users,
            IUnitOfWork uow)
        {
            _users = users;
            _uow = uow;
        }

        public async Task<int?> Handle(CreateUserCommand cmd)
        {
            Clear();
            cmd.Validate();
            if (!cmd.IsValid)
            {
                AddNotifications(cmd.Notifications);
                return null;
            }

            var exists = await _users.GetByEmail(cmd.CompanyId, cmd.Email);
            if (exists != null)
            {
                AddNotification("Email", "Já existe um usuário com este email");
                return null;
            }

            var user = new User
            {
                CompanyId = cmd.CompanyId,
                Name = cmd.Name,
                Email = cmd.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(cmd.Password)
            };

            await _users.Insert(user);
            await _uow.CommitAsync();
            return user.Id;
        }
    }
}
