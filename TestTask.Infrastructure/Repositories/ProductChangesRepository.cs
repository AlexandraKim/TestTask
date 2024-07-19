using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestTask.Core.Entities;
using TestTask.Core.Interfaces.Repositories;
using TestTask.Core.Utility;
using TestTask.Infrastructure.Persistence;

namespace TestTask.Infrastructure.Repositories;

internal class ProductChangesRepository(AppDbContext dbContext) : RepositoryBase<ProductChange>(dbContext), IProductChangesRepository
{
  public async Task<IEnumerable<ProductChange>> GetChangesByProductId(int productId)
  {
    return await _entities.Where(x => x.ProductId == productId).ToArrayAsync();
  }

  public Task<IEnumerable<ProductChange>> GetProductChangesForRangeAsync(DateTime from, DateTime to)
  {
    throw new NotImplementedException();
  }

  public async Task LogChangeAsync(Product product, ChangeType changeType, Guid userId)
  {
     dbContext.ProductChanges.Add(new ProductChange
     {
       ProductId = product.Id,
       UserId = userId,
       ChangeType = changeType,
       Date = DateTime.Now,
       Value = JsonConvert.SerializeObject(product)
     });
     await dbContext.SaveChangesAsync();
  }
}