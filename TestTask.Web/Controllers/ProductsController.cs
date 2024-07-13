using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    return View(products);
  }

  [Authorize(Roles = Constants.RoleAdmin)]
  public IActionResult ProductUpdate()
  {
    throw new NotImplementedException();
  }

  [Authorize(Roles = Constants.RoleAdmin)]
  public IActionResult ProductDelete()
  {
    throw new NotImplementedException();
  }
}