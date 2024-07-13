using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Models.Dto;
using TestTask.Service.IService;

namespace TestTask.Service;

public class ProductService(AppDbContext db, IMapper mapper) : IProductService
{
  public async Task<ResponseDto?> GetAllProductsAsync()
  {
    try
    {
      var products = await db.Products.Select(x => mapper.Map<ProductDto>(x))
        .ToArrayAsync();
      return new ResponseDto
      {
        Result = products
      };
    }
    catch (Exception e)
    {
      return new ResponseDto
      {
        IsSuccess = false,
        Message = "An error occurred: " + e.Message
      };
    }
  }

  public async Task<ResponseDto?> GetProductByIdAsync(int id)
  {
    try
    {
      var product = await db.Products.FirstAsync(x => x.ProductId == id);
      return new ResponseDto
      {
        Result = mapper.Map<ProductDto>(product)
      };
    }
    catch (Exception e)
    {
      return new ResponseDto
      {
        IsSuccess = false,
        Message = "An error occurred: " + e.Message
      };
    }
  }

  public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
  {
    try
    {
      var product = mapper.Map<Product>(productDto);
      db.Products.Add(product);
      await db.SaveChangesAsync();

      return new ResponseDto
      {
        Message = "Product created successfully!"
      };
    }
    catch (Exception e)
    {
      return new ResponseDto
      {
        IsSuccess = false,
        Message = "An error occurred during creation: " + e.Message
      };
    }
  }

  public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
  {
    try
    {
      var product = mapper.Map<Product>(productDto);

      db.Products.Update(product);
      await db.SaveChangesAsync();

      return new ResponseDto
      {
        Message = "Product updated successfully!"
      };
    }
    catch (Exception e)
    {
      return new ResponseDto
      {
        IsSuccess = false,
        Message = "An error occurred during update: " + e.Message
      };
    }
  }

  public async Task<ResponseDto?> DeleteAsync(int id)
  {
    try
    {
      var product = db.Products.First(x => x.ProductId == id);
      db.Products.Remove(product);
      await db.SaveChangesAsync();

      return new ResponseDto
      {
        Message = "Product deleted successfully!"
      };
    }
    catch (Exception e)
    {
      return new ResponseDto
      {
        IsSuccess = false,
        Message = "An error occurred during deletion: " + e.Message
      };
    }
  }
}