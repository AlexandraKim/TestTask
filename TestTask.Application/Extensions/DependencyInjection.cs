using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Products;

namespace TestTask.Application.Extensions;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddTransient<IProductService, ProductService>();

    return services;
  }
}