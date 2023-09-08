using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SP.Web.Site.Model;

namespace SP.Web.Site.Features.Account.Login;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly SignInManager<StuffPackerUser> _signInManager;
    private readonly UserManager<StuffPackerUser> _userManager;

    public LoginController(
        ILogger<LoginController> logger,
        UserManager<StuffPackerUser> userManager,
        SignInManager<StuffPackerUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Route("Login")]
    public IActionResult Login()
    {
        return View("Login");
    }

    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }

    [Route("Login")]
    [HttpPost]
    public async Task<IActionResult> LoginPostAsync(LoginViewModel inputModel, string returnUrl = null!)
    {
        returnUrl = returnUrl ?? Url.Content("/");

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(inputModel.Email);

            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var c1 = claims.Where(x => x.Type == "SpAdminApiToken").ToList();
                if (c1.Any())
                {
                    await _userManager.RemoveClaimsAsync(user, c1);
                }

                // var tokenC = new Claim("SpAdminApiToken", GetToken());
                // await _userManager.AddClaimAsync(user, tokenC);
                var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, inputModel.Password);
                if (passwordIsCorrect)
                {
                    var customClaims = new List<Claim>();

                    // {
                    //     tokenC
                    // };
                    await _signInManager.SignInWithClaimsAsync(user, inputModel.RememberMe, customClaims);
                    _logger.LogInformation(1, "User logged in.");
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View("Login");
        }

        // If we got this far, something failed, redisplay form
        return View("Login");
    }
}