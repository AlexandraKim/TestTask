using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestTask.Data;
using TestTask.Models;
using TestTask.Models.Dto;
using TestTask.Models.Utility;
using TestTask.Service.IService;
using TestTask.Utility;

namespace TestTask.Service;

public class ProductService(AppDbContext db, IMapper mapper, IProductChangeService productChangeService) : IProductService
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
    await using var transaction = await db.Database.BeginTransactionAsync();
    try
    {
      var product = mapper.Map<Product>(productDto);
      db.Products.Add(product);
      await db.SaveChangesAsync();

      await productChangeService.LogChangeAsync(product, ChangeType.Create);
      
      await transaction.CommitAsync();
      return new ResponseDto
      {
        Result = productDto,
        Message = "Product created successfully!"
      };
    }
    catch (Exception e)
    {
      await transaction.RollbackAsync();
      return new ResponseDto
      {
        IsSuccess = false,
        Message = "An error occurred during creation: " + e.Message
      };
    }
  }

  public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
  {
    await using var transaction = await db.Database.BeginTransactionAsync();
    try
    {
      var product = mapper.Map<Product>(productDto);
      db.Products.Update(product);
      await db.SaveChangesAsync();

      await productChangeService.LogChangeAsync(product, ChangeType.Update);
      
      await transaction.CommitAsync();
      return new ResponseDto
      {
        Message = "Product updated successfully!"
      };
    }
    catch (Exception e)
    {
      await transaction.RollbackAsync();
      return new ResponseDto
      {
        IsSuccess = false,
        Message = "An error occurred during update: " + e.Message
      };
    }
  }

  public async Task<ResponseDto?> DeleteAsync(int id)
  {
    await using var transaction = await db.Database.BeginTransactionAsync();
    try
    {
      var product = db.Products.First(x => x.ProductId == id);
      db.Products.Remove(product);
      await db.SaveChangesAsync();
      
      await productChangeService.LogChangeAsync(product, ChangeType.Delete);
      
      await transaction.CommitAsync();

      return new ResponseDto
      {
        Message = "Product deleted successfully!"
      };
    }
    catch (Exception e)
    {
      await transaction.RollbackAsync();
      return new ResponseDto
      {
        IsSuccess = false,
        Message = "An error occurred during deletion: " + e.Message
      };
    }
  }
}