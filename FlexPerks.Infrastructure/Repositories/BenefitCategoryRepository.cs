using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using FlexPerks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPerks.Infrastructure.Repositories
{
    public class BenefitCategoryRepository : GenericRepository<BenefitCategory>, IBenefitCategoryRepository
    {
        public BenefitCategoryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<BenefitCategory?> GetByName(string name)
        {
            return await _dbContext.BenefitCategories
                .SingleOrDefaultAsync(b => b.Name == name);
        }
    }
}
