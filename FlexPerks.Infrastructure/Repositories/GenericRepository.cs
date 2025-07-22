using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using FlexPerks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPerks.Infrastructure.Repositories
{
    public class GenericRepository<T> : IAsyncRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Insert(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> Update(T item)
        {
            _dbContext.Set<T>().Update(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(T item)
        {
            _dbContext.Set<T>().Remove(item);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
    }
}
