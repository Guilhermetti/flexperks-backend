using FlexPerks.Domain.Models;
using System.Linq.Expressions;

namespace FlexPerks.Application.Queries
{
    public static class EmployeeQueries
    {
        public static Expression<Func<Employee, bool>> ByCompanyAndEmail(int companyId, string email)
            => e => e.CompanyId == companyId && e.Email == email;

        public static Expression<Func<Employee, bool>> ByCompany(int companyId)
            => e => e.CompanyId == companyId;
    }
}
