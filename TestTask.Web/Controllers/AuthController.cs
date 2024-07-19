using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using TestTask.Application.Dtos;
using TestTask.Application.Identity;
using TestTask.Models.Dto;
using Constants = TestTask.Application.Utility.Constants;

namespace TestTask.Controllers;

public class AuthController(
  IIdentityService identityService
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
    var loginResponse = await identityService.LoginAsync(loginRequestDto);
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
    var result = await identityService.RegisterAsync(registrationRequestDto);
    if (result.IsNullOrEmpty())
    {
      if (string.IsNullOrEmpty(registrationRequestDto.Role)) registrationRequestDto.Role = Constants.RoleUser;

      TempData["success"] = "Registration Successful";
      return RedirectToAction("Index", "Products");
    }

    TempData["error"] = result;

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
    await identityService.LogoutAsync();
    return RedirectToAction("Index", "Home");
  }
}