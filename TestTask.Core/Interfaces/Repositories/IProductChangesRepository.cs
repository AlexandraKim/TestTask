using TestTask.Core.Entities;
using TestTask.Core.Utility;

namespace TestTask.Core.Interfaces.Repositories;

public interface IProductChangesRepository : IRepositoryBase<ProductChange>
{
  Task<IEnumerable<ProductChange>> GetChangesByProductId(int productId);
  Task<IEnumerable<ProductChange>> GetProductChangesForRangeAsync(DateTime from, DateTime to);
  Task LogChangeAsync(Product product, ChangeType changeType, Guid userId);
}