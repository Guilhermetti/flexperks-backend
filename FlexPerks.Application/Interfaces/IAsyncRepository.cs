using FlexPerks.Domain.Models;

namespace FlexPerks.Application.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseModel
    {
        Task<T> Insert(T item);
        Task<T> Update(T item);
        Task<bool> Delete(T item);
        Task<T?> GetById(int? id);
        Task<IEnumerable<T>> ListAll();
    }
}
