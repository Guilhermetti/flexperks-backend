using FlexPerks.Domain.Enums;
using Flunt.Notifications;
using Flunt.Validations;

namespace FlexPerks.Application.Commands.TimeClock
{
    public class CreateTimeClockEntryCommand : Notifiable<Notification>
    {
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }

        public DateTime TimestampUtc { get; set; }

        public TimeClockEntryType Type { get; set; }
        public TimeClockEntrySource Source { get; set; } = TimeClockEntrySource.Web;

        public string? Note { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<CreateTimeClockEntryCommand>()
                .IsGreaterThan(CompanyId, 0, nameof(CompanyId), "Empresa inválida")
                .IsGreaterThan(EmployeeId, 0, nameof(EmployeeId), "Colaborador inválido")
                .IsNotNull(TimestampUtc, nameof(TimestampUtc), "Timestamp é obrigatório")
                .IsLowerOrEqualsThan(TimestampUtc, DateTime.UtcNow.AddMinutes(2), nameof(TimestampUtc), "Batida não pode ser futura")
                .IsGreaterThan((int)Type, 0, nameof(Type), "Tipo de batida inválido")
                .IsGreaterThan((int)Source, 0, nameof(Source), "Origem inválida")
            );

            if (!string.IsNullOrWhiteSpace(Note) && Note.Length > 300)
                AddNotification(nameof(Note), "Observação deve ter no máximo 300 caracteres");
        }
    }
}
