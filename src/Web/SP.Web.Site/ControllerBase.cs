using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SP.Shared.Common;

namespace SP.Web.Site;

public class ControllerBase : Controller
{
    protected Guid GetUserId()
    {
        if (User == null)
        {
            return Guid.Empty;
        }

        if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            return Guid.Parse(id);
        }

        return Guid.Empty;
    }
}