namespace TestTask.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
  Task RunInTransactionAsync(Func<Task> action);
}