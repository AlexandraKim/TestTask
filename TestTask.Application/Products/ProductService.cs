using AutoMapper;
using TestTask.Application.Dtos;
using TestTask.Core.Entities;
using TestTask.Core.Interfaces;
using TestTask.Core.Interfaces.Repositories;
using TestTask.Core.Utility;

namespace TestTask.Application.Products;

public class ProductService(
  IProductsRepository productsRepository, 
  IProductChangesRepository productChangesRepository,
  IUnitOfWork unitOfWork,
  IMapper mapper) : IProductService
{
  public async Task<ResponseDto?> GetAllProductsAsync()
  {
    try
    {
      var products = (await productsRepository.GetAllAsync())
        .Select(mapper.Map<ProductDto>);
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
      var product = await productsRepository.GetByIdAsync(id);
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

  public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto, Guid userId)
  {
    try
    {
      await unitOfWork.RunInTransactionAsync(async () =>
      {
        var product = mapper.Map<Product>(productDto);
        await productsRepository.AddAsync(product);
        await productChangesRepository.LogChangeAsync(product, ChangeType.Create, userId);
      });
    
      return new ResponseDto
      {
        Result = productDto,
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

  public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto, Guid userId)
  {
    try
    {
      await unitOfWork.RunInTransactionAsync(async () =>
      {
        var product = mapper.Map<Product>(productDto);
        await productsRepository.UpdateAsync(product);
        await productChangesRepository.LogChangeAsync(product, ChangeType.Update, userId);
      });
      
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

  public async Task<ResponseDto?> DeleteAsync(int id, Guid userId)
  {
    try
    {
      await unitOfWork.RunInTransactionAsync(async () =>
      {
        var product = await productsRepository.GetByIdAsync(id);
        await productsRepository.DeleteAsync(product!);
        await productChangesRepository.LogChangeAsync(product!, ChangeType.Delete, userId);
      });

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