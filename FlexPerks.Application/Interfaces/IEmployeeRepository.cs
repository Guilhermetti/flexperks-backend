using FlexPerks.Domain.Models;

namespace FlexPerks.Application.Interfaces
{
    public interface IEmployeeRepository : IAsyncRepository<Employee>
    {
        Task<Employee?> GetByEmail(int companyId, string email);
    }
}
