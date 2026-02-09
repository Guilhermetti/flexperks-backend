using FlexPerks.Application.Commands.Employees;
using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using Flunt.Notifications;

namespace FlexPerks.Application.Handlers
{
    public class EmployeeHandler : Notifiable<Notification>
    {
        private readonly IEmployeeRepository _employees;
        private readonly IUnitOfWork _uow;

        public EmployeeHandler(IEmployeeRepository employees, IUnitOfWork uow)
        {
            _employees = employees;
            _uow = uow;
        }

        public async Task<int?> Handle(CreateEmployeeCommand cmd)
        {
            Clear();
            cmd.Validate();
            if (!cmd.IsValid)
            {
                AddNotifications(cmd.Notifications);
                return null;
            }

            var exists = await _employees.GetByEmail(cmd.CompanyId, cmd.Email);
            if (exists != null)
            {
                AddNotification("Email", "Já existe um colaborador com este email nesta empresa");
                return null;
            }

            var employee = new Employee
            {
                CompanyId = cmd.CompanyId,
                FullName = cmd.FullName,
                Email = cmd.Email,
                Document = cmd.Document,
                Registration = cmd.Registration,
                HireDate = cmd.HireDate,
                ManagerId = cmd.ManagerId
            };

            await _employees.Insert(employee);
            await _uow.CommitAsync();
            return employee.Id;
        }
    }
}
