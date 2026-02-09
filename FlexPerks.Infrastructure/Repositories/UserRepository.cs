using FlexPerks.Application.Interfaces;
using FlexPerks.Application.Queries;
using FlexPerks.Domain.Models;
using FlexPerks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPerks.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<User?> GetByEmail(int companyId, string email)
        {
            return await _dbContext.Users
                .SingleOrDefaultAsync(UserQueries.ByCompanyAndEmail(companyId, email));
        }
    }
}
