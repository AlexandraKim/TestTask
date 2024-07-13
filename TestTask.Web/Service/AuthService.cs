using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TestTask.Data;
using TestTask.Models;
using TestTask.Models.Dto;
using TestTask.Service.IService;

namespace TestTask.Service;

public class AuthService(
  AppDbContext dbContext,
  UserManager<ApplicationUser> userManager,
  SignInManager<ApplicationUser> signInManager,
  RoleManager<IdentityRole> roleManager)
  : IAuthService
{
  public async Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto)
  {
    ApplicationUser user = new()
    {
      UserName = registrationRequestDto.Email,
      Email = registrationRequestDto.Email,
      NormalizedEmail = registrationRequestDto.Email.ToUpper(),
      Name = registrationRequestDto.Name,
      PhoneNumber = registrationRequestDto.PhoneNumber,
      
    };
    try
    {
      var result = await userManager.CreateAsync(user, registrationRequestDto.Password);
      if (result.Succeeded)
      {
        if (!registrationRequestDto.Role.IsNullOrEmpty())
        {
          await AssignRoleAsync(user.Email, registrationRequestDto.Role);
        }
        await signInManager.SignInAsync(user, isPersistent: false);

        return "";
      }

      return result.Errors.FirstOrDefault()!.Description;
    }
    catch (Exception e)
    {
      return "Error encountered";
    }

  }

  public async Task<bool> LoginAsync(LoginRequestDto loginRequestDto)
  {
    var user = dbContext.ApplicationUsers.FirstOrDefault(
      u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
    
    var result = await signInManager.PasswordSignInAsync(user.Email,
      loginRequestDto.Password, isPersistent: true, lockoutOnFailure: true);

      return result.Succeeded;
  }

  public async Task LogoutAsync()
  {
    await signInManager.SignOutAsync();
  }

  private async Task<bool> AssignRoleAsync(string email, string roleName)
  {
    var user = dbContext.ApplicationUsers.FirstOrDefault(
      u => u.UserName.ToLower() == email.ToLower());
    if (user is not null)
    {
      if (!(await roleManager.RoleExistsAsync(roleName)))
      {
        await roleManager.CreateAsync(new IdentityRole(roleName));
      }

      await userManager.AddToRoleAsync(user, roleName);
      return true;
    }

    return false;
  }
}