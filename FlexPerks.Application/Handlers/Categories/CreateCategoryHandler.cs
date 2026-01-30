using FlexPerks.Application.Commands.Categories;
using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using Flunt.Notifications;

namespace FlexPerks.Application.Handlers.Categories
{
    public class CreateCategoryHandler : Notifiable<Notification>
    {
        private readonly IBenefitCategoryRepository _categories;
        private readonly IUnitOfWork _uow;
        public CreateCategoryHandler(
            IBenefitCategoryRepository categories,
            IUnitOfWork uow)
        {
            _categories = categories;
            _uow = uow;
        }

        public async Task<int?> Handle(CreateCategoryCommand cmd)
        {
            cmd.Validate();
            if (!cmd.IsValid)
            {
                AddNotifications(cmd.Notifications);
                return null;
            }

            var existing = await _categories.GetByName(cmd.Name);
            if (existing != null)
            {
                AddNotification("Name", "Categoria já existe");
                return null;
            }

            var cat = new BenefitCategory { Name = cmd.Name };
            await _categories.Insert(cat);
            await _uow.CommitAsync();
            return cat.Id;
        }
    }
}
