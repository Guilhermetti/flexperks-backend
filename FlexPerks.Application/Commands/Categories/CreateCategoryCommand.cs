using Flunt.Notifications;
using Flunt.Validations;

namespace FlexPerks.Application.Commands.Categories
{
    public class CreateCategoryCommand : Notifiable<Notification>
    {
        public string Name { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(new Contract<CreateCategoryCommand>()
            .IsNotNullOrWhiteSpace(Name, nameof(Name), "Nome é obrigatório")
            .IsGreaterOrEqualsThan(Name, 2, nameof(Name), "Nome muito curto"));
        }
    }
}
