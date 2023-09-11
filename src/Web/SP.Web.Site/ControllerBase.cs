using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SP.Web.Site;

public class ControllerBase : Controller
{
    protected Guid GetUserId()
    {
        if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        }

        return Guid.Empty;
    }
}