using TestTask.Models;
using TestTask.Models.Utility;

namespace TestTask.Service.IService;

public interface IProductChangeService
{
  public Task<IEnumerable<ProductChange>> GetProductChangesForRangeAsync(DateTime from, DateTime to);
  public Task LogChangeAsync(Product product, ChangeType changeType);
}