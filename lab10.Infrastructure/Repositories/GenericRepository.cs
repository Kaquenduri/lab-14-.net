using lab10.Application.Repositories;
using lab10.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace lab10.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class 
{
    protected readonly AppDbContext _dbContext;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
        
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task Add(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
    
}