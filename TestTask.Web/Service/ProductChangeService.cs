using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestTask.Data;
using TestTask.Models;
using TestTask.Models.Utility;
using TestTask.Service.IService;
using TestTask.Utility;

namespace TestTask.Service;

public class ProductChangeService(AppDbContext dbContext, IHttpContextAccessor  httpContextAccessor) : IProductChangeService
{
  public async Task<IEnumerable<ProductChange>> GetProductChangesForRangeAsync(DateTime from, DateTime to)
  {
    return await dbContext.ProductChanges.Where(x => x.Date > from && x.Date < to).ToArrayAsync();
  }

  public async Task LogChangeAsync(Product product, ChangeType changeType)
  {
    var userId = GetUserId();
    dbContext.ProductChanges.Add(new ProductChange
    {
      ProductId = product.ProductId,
      UserId = userId,
      ChangeType = changeType,
      Date = DateTime.Now,
      Value = JsonConvert.SerializeObject(product)
    });
    await dbContext.SaveChangesAsync();
  }
  
  
  private Guid GetUserId()
  {
    return httpContextAccessor.HttpContext.User.GetLoggedInUserId();
  }
}