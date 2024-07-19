using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Infrastructure.Persistence;

namespace TestTask.Infrastructure.Extensions;

public static class MigrationExtensions
{
  public static async Task ApplyMigrationsAsync(this IServiceProvider services)
  {
    using var scope = services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (db.Database.GetPendingMigrations().Any()) await db.Database.MigrateAsync();
  }
}