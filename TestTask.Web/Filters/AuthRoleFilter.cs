using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestTask.Filters;

public class AuthRoleFilter : ActionFilterAttribute, IAuthorizationFilter
{
  public AuthRoleFilter(string roleClaim)
  {
    RoleClaims = new[] {roleClaim};
  }

  public AuthRoleFilter(string[] roleClaims)
  {
    RoleClaims = roleClaims;
  }

  private string[] RoleClaims { get; set; }

  public void OnAuthorization(AuthorizationFilterContext context)
  {
    if (!context.HttpContext.User.Identity.IsAuthenticated)
    {
      context.Result = new UnauthorizedResult();
    }

    var hasAny = false;
    foreach (var role in RoleClaims)
    {
      try
      {
        var roleId = context.HttpContext.User.FindFirstValue(role + "Id");
        if (roleId != null)
        {
          hasAny = true;
          break;
        }
      }
      catch (Exception e)
      {
        continue;
      }
    }

    if (!hasAny)
    {
      context.Result = new UnauthorizedResult();
    }
  }
}