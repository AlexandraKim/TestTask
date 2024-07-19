using Microsoft.AspNetCore.Identity;

namespace TestTask.Core.Entities;

public class ApplicationUser : IdentityUser
{
  public string Name { get; set; }
}