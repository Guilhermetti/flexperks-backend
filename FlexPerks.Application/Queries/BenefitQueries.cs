using FlexPerks.Domain.Models;
using System.Linq.Expressions;

namespace FlexPerks.Application.Queries
{
    public static class BenefitQueries
    {
        public static Expression<Func<BenefitCategory, bool>> GetByName(string name)
            => b => b.Name == name;
    }
}
