using FlexPerks.Domain.Models;
using System.Linq.Expressions;

namespace FlexPerks.Application.Queries
{
    public static class UserQueries
    {
        public static Expression<Func<User, bool>> ByCompanyAndEmail(int companyId, string email)
            => u => u.CompanyId == companyId && u.Email == email;
    }
}
