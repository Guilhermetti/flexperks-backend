using FlexPerks.Domain.Models;

namespace FlexPerks.Application.Interfaces
{
    public interface IBenefitCategoryRepository : IAsyncRepository<BenefitCategory>
    {
        Task<BenefitCategory?> GetByName(string name);
    }
}
