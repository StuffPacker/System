using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Sp.Api.Host.Controllers;

public class SpControllerBase : ControllerBase
{
    public Guid GetUser()
    {
        if (User == null)
        {
            throw new AuthenticationException("No User pressent");
        }

        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        if (user == null)
        {
            throw new AuthenticationException("No userid pressent");
        }

        var parsed = Guid.TryParse(user!.Value, out var validUser);
        if (parsed == false)
        {
            throw new Exception("Cant parse userId");
        }

        return validUser;
    }
}