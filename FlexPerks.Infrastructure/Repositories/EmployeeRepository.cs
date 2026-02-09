using FlexPerks.Application.Interfaces;
using FlexPerks.Application.Queries;
using FlexPerks.Domain.Models;
using FlexPerks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPerks.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<Employee?> GetByEmail(int companyId, string email)
        {
            return await _dbContext.Employees
                .SingleOrDefaultAsync(EmployeeQueries.ByCompanyAndEmail(companyId, email));
        }
    }
}
