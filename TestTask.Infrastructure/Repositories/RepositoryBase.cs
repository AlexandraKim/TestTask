using Microsoft.EntityFrameworkCore;
using TestTask.Core.Entities;
using TestTask.Core.Interfaces.Repositories;
using TestTask.Infrastructure.Extensions;
using TestTask.Infrastructure.Persistence;

namespace TestTask.Infrastructure.Repositories;

internal abstract class RepositoryBase<T>(AppDbContext dbContext) : IRepositoryBase<T> where T : EntityBase 
{
  protected readonly DbSet<T> _entities = dbContext.GetDbSet<T>();
  public async Task<T?> GetByIdAsync(int id)
  {
    return await _entities.FirstOrDefaultAsync(x => x.Id == id);
  }

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await _entities.ToArrayAsync();
  }

  public async Task AddAsync(T entity)
  {
    await _entities.AddAsync(entity);
    await dbContext.SaveChangesAsync();
  }

  public async Task UpdateAsync(T entity)
  {
    _entities.Update(entity);
    await dbContext.SaveChangesAsync();
  }

  public async Task DeleteAsync(T entity)
  {
    _entities.Remove(entity);
    await dbContext.SaveChangesAsync();
  }
}