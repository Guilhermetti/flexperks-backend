using FlexPerks.Application.Commands.TimeClock;
using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using Flunt.Notifications;

namespace FlexPerks.Application.Handlers
{
    public class TimeClockEntryHandler : Notifiable<Notification>
    {
        private readonly ITimeClockEntryRepository _entries;
        private readonly IEmployeeRepository _employees;
        private readonly IUnitOfWork _uow;

        public TimeClockEntryHandler(
            ITimeClockEntryRepository entries,
            IEmployeeRepository employees,
            IUnitOfWork uow)
        {
            _entries = entries;
            _employees = employees;
            _uow = uow;
        }

        public async Task<int?> Handle(CreateTimeClockEntryCommand cmd)
        {
            Clear();
            cmd.Validate();
            if (!cmd.IsValid)
            {
                AddNotifications(cmd.Notifications);
                return null;
            }

            var employee = await _employees.GetById(cmd.EmployeeId);
            if (employee is null || employee.CompanyId != cmd.CompanyId)
            {
                AddNotification(nameof(cmd.EmployeeId), "Colaborador não encontrado para a empresa informada");
                return null;
            }

            var duplicate = await _entries.ExistsSamePunch(cmd.CompanyId, cmd.EmployeeId, cmd.TimestampUtc, cmd.Type);
            if (duplicate)
            {
                AddNotification("TimeClock", "Já existe uma batida idêntica para este colaborador");
                return null;
            }

            var entry = new TimeClockEntry
            {
                CompanyId = cmd.CompanyId,
                EmployeeId = cmd.EmployeeId,
                TimestampUtc = cmd.TimestampUtc,
                Type = cmd.Type,
                Source = cmd.Source,
                Note = cmd.Note
            };

            await _entries.Insert(entry);
            await _uow.CommitAsync();
            return entry.Id;
        }
    }
}
