using TestTask.Models;
using TestTask.Models.Dto;

namespace TestTask.Service.IService;

public interface IProductService
{
  Task<IEnumerable<ProductDto>> GetAllProductsAsync();
  Task<ProductDto?> GetProductByIdAsync(int id);
  Task<ProductDto?> CreateProductAsync(ProductDto productDto);
  Task<ProductDto?> UpdateProductAsync(ProductDto productDto);
  Task<bool> DeleteAsync(int id);
}