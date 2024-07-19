using TestTask.Core.Entities;
using TestTask.Core.Interfaces.Repositories;
using TestTask.Infrastructure.Persistence;

namespace TestTask.Infrastructure.Repositories;

internal class ProductsRepository(AppDbContext dbContext) : RepositoryBase<Product>(dbContext), IProductsRepository
{
}