using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Infrastructure.Persistence;

namespace TestTask.Infrastructure.Extensions;

// public static class MigrationExtensions
// {
//   public static async Task ApplyMigrationsAsync(WebApplication app)
//   {
//     using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
//
//     using (var scope = services.CreateScope())
//     {
//       var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//       if (_db.Database.GetPendingMigrations().Any()) _db.Database.Migrate();
//     }
//   }
// }