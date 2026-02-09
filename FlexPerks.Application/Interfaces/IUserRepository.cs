using FlexPerks.Domain.Models;

namespace FlexPerks.Application.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User?> GetByEmail(int companyId, string email);
    }
}
