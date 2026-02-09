using Flunt.Notifications;
using Flunt.Validations;

namespace FlexPerks.Application.Commands.Employees
{
    public class CreateEmployeeCommand : Notifiable<Notification>
    {
        public int CompanyId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string? Document { get; set; }
        public string? Registration { get; set; }

        public DateTime HireDate { get; set; } = DateTime.UtcNow.Date;
        public int? ManagerId { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<CreateEmployeeCommand>()
                .IsGreaterThan(CompanyId, 0, nameof(CompanyId), "Empresa inválida")
                .IsNotNullOrWhiteSpace(FullName, nameof(FullName), "Nome é obrigatório")
                .IsGreaterOrEqualsThan(FullName, 3, nameof(FullName), "Nome deve ter ao menos 3 caracteres")
                .IsNotNullOrWhiteSpace(Email, nameof(Email), "Email é obrigatório")
                .IsEmail(Email, nameof(Email), "Email inválido")
                .IsLowerOrEqualsThan(HireDate, DateTime.UtcNow.Date, nameof(HireDate), "Data de admissão não pode ser futura")
            );
        }
    }
}
