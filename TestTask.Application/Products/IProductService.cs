using TestTask.Application.Dtos;

namespace TestTask.Application.Products;

public interface IProductService
{
  Task<ResponseDto?> GetAllProductsAsync();
  Task<ResponseDto?> GetProductByIdAsync(int id);
  Task<ResponseDto?> CreateProductAsync(ProductDto productDto, Guid userId);
  Task<ResponseDto?> UpdateProductAsync(ProductDto productDto, Guid userId);
  Task<ResponseDto?> DeleteAsync(int id, Guid userId);
}