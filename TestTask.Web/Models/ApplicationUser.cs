using Microsoft.AspNetCore.Identity;

namespace TestTask.Models;

public class ApplicationUser : IdentityUser
{
  public string Name { get; set; }
}