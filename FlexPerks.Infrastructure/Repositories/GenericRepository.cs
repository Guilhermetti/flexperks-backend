using FlexPerks.Application.Interfaces;
using FlexPerks.Domain.Models;
using FlexPerks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IAsyncRepository<T> where T : BaseModel
{
    protected readonly ApplicationDbContext _dbContext;
    public GenericRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<T> Insert(T item)
    {
        await _dbContext.Set<T>().AddAsync(item);
        return item;
    }

    public Task<T> Update(T item)
    {
        _dbContext.Set<T>().Update(item);
        return Task.FromResult(item);
    }

    public Task<bool> Delete(T item)
    {
        _dbContext.Set<T>().Remove(item);
        return Task.FromResult(true);
    }

    public async Task<T> GetById(int id) => await _dbContext.Set<T>().FindAsync(id);
    public async Task<IEnumerable<T>> ListAll() => await _dbContext.Set<T>().ToListAsync();
}
