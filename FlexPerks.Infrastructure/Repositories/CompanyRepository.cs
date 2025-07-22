using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using FlexPerks.Infrastructure.Data;

namespace FlexPerks.Infrastructure.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
