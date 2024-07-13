using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using TestTask.Models.Dto;
using TestTask.Service.IService;
using Constants = TestTask.Models.Utility.Constants;

namespace TestTask.Controllers;

public class AuthController(IAuthService authService
  ) : Controller
{
  [HttpGet]
  public IActionResult Login()
  {
    LoginRequestDto loginRequestDto = new();
    return View(loginRequestDto);
  }

  [HttpPost]
  public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
  {
    var loginResponse = await authService.LoginAsync(loginRequestDto);
    if (loginResponse)
      return RedirectToAction("Index", "Products");

    TempData["error"] = "Login failed";
    return View(loginRequestDto);
  }

  [HttpGet]
  public IActionResult Register()
  {
    var roleList = new List<SelectListItem>
    {
      new(Constants.RoleAdmin, Constants.RoleAdmin),
      new(Constants.RoleUser, Constants.RoleUser)
    };
    ViewBag.RoleList = roleList;
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
  {
    var result = await authService.RegisterAsync(registrationRequestDto);
    if (result.IsNullOrEmpty())
    {
      if (string.IsNullOrEmpty(registrationRequestDto.Role)) registrationRequestDto.Role = Constants.RoleUser;

      bool assignRoleResult = await authService.AssignRoleAsync(registrationRequestDto.Email, registrationRequestDto.Role);
      if (assignRoleResult)
      {
        TempData["success"] = "Registration Successful";
        return RedirectToAction("Index", "Products");
      }
    }
    else
    {
      TempData["error"] = result;
    }

    var roleList = new List<SelectListItem>
    {
      new(Constants.RoleAdmin, Constants.RoleAdmin),
      new(Constants.RoleUser, Constants.RoleUser)
    };
    ViewBag.RoleList = roleList;
    return View(registrationRequestDto);
  }

  [HttpGet]
  public async Task<IActionResult> Logout()
  {
    await authService.LogoutAsync();
    return RedirectToAction("Index", "Home");
  }
}