using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Core.Entities;
using TestTask.Infrastructure.Persistence;

namespace TestTask.Infrastructure.Extensions;

public static class ServiceCollectionsExtensions
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<AppDbContext>(option =>
    {
      option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    });
  }

  public static void AddIdentity(this IServiceCollection services)
  {
    services.AddIdentity<ApplicationUser, IdentityRole>()
      .AddEntityFrameworkStores<AppDbContext>()
      .AddDefaultTokenProviders();
  }
  
  public static void AddAutoMapper(this IServiceCollection services)
  {
    var mapper = MappingConfig.RegisterMaps().CreateMapper();
    services.AddSingleton(mapper);
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
  }
}