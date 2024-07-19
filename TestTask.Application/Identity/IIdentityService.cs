using TestTask.Application.Dtos;
using TestTask.Models.Dto;

namespace TestTask.Application.Identity;

public interface IIdentityService
{
  Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto);
  Task<bool> LoginAsync(LoginRequestDto loginRequestDto);
  Task LogoutAsync();
}