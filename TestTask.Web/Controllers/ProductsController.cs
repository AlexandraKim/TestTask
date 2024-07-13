using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Models.Dto;
using TestTask.Models.Utility;
using TestTask.Service.IService;

namespace TestTask.Controllers;

public class ProductsController(IProductService productService) : Controller
{
  // GET
  [Authorize]
  public async Task<IActionResult> Index()
  {
    var products = await productService.GetAllProductsAsync();
    return View(products.Result);
  }

  [Authorize(Roles = Constants.RoleAdmin)]
  public async Task<IActionResult> ProductUpdate(int id)
  {
    var response = await productService.GetProductByIdAsync(id);
    if (response is not null && response.IsSuccess)
    {
      var model = response.Result;
      return View(model);
    }

    TempData["error"] = response?.Message;
    return NotFound();
  }

  [HttpPost]
  [Authorize(Roles = Constants.RoleAdmin)]
  public async Task<IActionResult> ProductUpdate(ProductDto productDto)
  {
    if (ModelState.IsValid)
    {
      var response = await productService.UpdateProductAsync(productDto);

      if (response is not null && response.IsSuccess)
      {
        TempData["success"] = "Product updated successfully";
        return RedirectToAction(nameof(Index));
      }

      TempData["error"] = response.Message;
    }

    return View(productDto);
  }

  [Authorize(Roles = Constants.RoleAdmin)]
  public async Task<IActionResult> ProductDelete(int id)
  {
    var response = await productService.GetProductByIdAsync(id);

    if (response is not null && response.IsSuccess)
    {
      var model = response.Result;
      return View(model);
    }

    TempData["error"] = response?.Message;
    return NotFound();
  }
  
  [HttpPost]
  public async Task<IActionResult> ProductDelete(ProductDto productDto)
  {
    var response = await productService.DeleteAsync(productDto.ProductId);

    if (response is not null && response.IsSuccess)
    {
      TempData["success"] = response.Message;
      return RedirectToAction(nameof(Index));
    }

    TempData["error"] = response?.Message;
    return View(productDto);
  }

  [Authorize(Roles = Constants.RoleAdmin)]
  public IActionResult ProductCreate()
  {
    return View();
  }
  
  [HttpPost]
  [Authorize(Roles = Constants.RoleAdmin)]
  public async Task<IActionResult> ProductCreate(ProductDto productDto)
  {
    if (ModelState.IsValid)
    {
      var response = await productService.CreateProductAsync(productDto);
      if (response is not null && response.IsSuccess)
      {
        TempData["success"] = response.Message;
        return RedirectToAction(nameof(Index));
      }

      TempData["error"] = response?.Message;
    }

    return View(productDto);
  }
}