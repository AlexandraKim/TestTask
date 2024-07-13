using System.Security.Claims;

namespace TestTask.Utility;

public static class ClaimsPrincipalExtensions
{
  public static Guid GetLoggedInUserId(this ClaimsPrincipal principal)
  {
    if (principal == null)
      throw new ArgumentNullException(nameof(principal));

    var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

    return new Guid(loggedInUserId);
  }
}