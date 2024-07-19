namespace TestTask.Core.Interfaces.Repositories;

public interface IRepositoryBase<T>
{
  Task<T?> GetByIdAsync(int id);
  Task<IEnumerable<T>> GetAllAsync();
  Task AddAsync(T entity);
  Task UpdateAsync(T entity);
  Task DeleteAsync(T entity);
}