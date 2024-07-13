using TestTask.Models.Dto;

namespace TestTask.Service.IService;

public interface IAuthService
{
  Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto);
  Task<bool> LoginAsync(LoginRequestDto loginRequestDto);
  Task LogoutAsync();
}