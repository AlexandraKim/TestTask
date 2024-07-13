using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models.Dto;
using TestTask.Service.IService;

namespace TestTask.Service;

public class ProductService(AppDbContext db, IMapper mapper) : IProductService
{
  public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
  {
    return await db.Products.Select(x => mapper.Map<ProductDto>(x))
      .ToArrayAsync();
  }

  public async Task<ProductDto?> GetProductByIdAsync(int id)
  {
    return mapper.Map<ProductDto>(await db.Products.FirstOrDefaultAsync(x => x.ProductId == id));
  }

  public Task<ProductDto?> CreateProductAsync(ProductDto productDto)
  {
    throw new NotImplementedException();
  }

  public Task<ProductDto?> UpdateProductAsync(ProductDto productDto)
  {
    throw new NotImplementedException();
  }

  public Task<bool> DeleteAsync(int id)
  {
    throw new NotImplementedException();
  }
}