using FlexPerks.Domain.Models;
using System.Linq.Expressions;

namespace FlexPerks.Application.Queries
{
    public static class UserQueries
    {
        public static Expression<Func<User, bool>> ByEmail(string email)
            => u => u.Email == email;
    }
}
