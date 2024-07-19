using TestTask.Core.Interfaces;
using TestTask.Infrastructure.Persistence;

namespace TestTask.Infrastructure;

internal class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
  public void Dispose()
  {
    dbContext.Dispose();
  }

  public async Task RunInTransactionAsync(Func<Task> action)
  {
    var transaction = await dbContext.Database.BeginTransactionAsync();

    try
    {
      await action.Invoke();
      await transaction.CommitAsync();
    }
    catch (Exception e)
    {
      await transaction.RollbackAsync();
      Console.WriteLine(e);
      throw;
    }
  }

  public async Task SaveAsync()
  {
    await dbContext.SaveChangesAsync();
  }
}