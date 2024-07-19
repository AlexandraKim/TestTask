using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Identity;
using TestTask.Core.Entities;
using TestTask.Core.Interfaces;
using TestTask.Core.Interfaces.Repositories;
using TestTask.Core.Utility;
using TestTask.Infrastructure.Identity;
using TestTask.Infrastructure.Persistence;
using TestTask.Infrastructure.Repositories;
using TestTask.Infrastructure.Services;

namespace TestTask.Infrastructure.Extensions;

public static class ServiceCollectionsExtensions
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<AppDbContext>(option =>
    {
      option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    });
    
    services.AddIdentity();

    services.AddAutoMapper();

    services.AddRepositories();
  }

  private static void AddIdentity(this IServiceCollection services)
  {
    services.AddIdentity<ApplicationUser, IdentityRole>()
      .AddEntityFrameworkStores<AppDbContext>()
      .AddDefaultTokenProviders();
 
    services.AddTransient<IIdentityService, IdentityService>();
  }
  
  private static void AddAutoMapper(this IServiceCollection services)
  {
    var mapper = MappingConfig.RegisterMaps().CreateMapper();
    services.AddSingleton(mapper);
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
  }

  private static void AddRepositories(this IServiceCollection services)
  {
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IProductsRepository, ProductsRepository>();
    services.AddScoped<IProductChangesRepository, ProductChangesRepository>();
  }
}