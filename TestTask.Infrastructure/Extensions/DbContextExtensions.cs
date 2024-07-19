using Microsoft.EntityFrameworkCore;

namespace TestTask.Infrastructure.Extensions;

internal static class DbContextExtensions
{
  public static DbSet<T> GetDbSet<T>(this DbContext context) where T : class
  {
    var properties = context.GetType().GetProperties();
    var dbSetProperty = properties.FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>));
    return dbSetProperty?.GetValue(context) as DbSet<T>;
  }
}