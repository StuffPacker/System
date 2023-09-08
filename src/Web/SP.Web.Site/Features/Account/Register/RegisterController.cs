using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Site.Model;

namespace SP.Web.Site.Features.Account.Register;

[Route("account")]
public class RegisterController : Controller
{
    private readonly SignInManager<StuffPackerUser> _signInManager;
    private readonly UserManager<StuffPackerUser> _userManager;
    private readonly IUserStore<StuffPackerUser> _userStore;

    // private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly ILogger<RegisterController> _logger;

    // private readonly IEmailSender _emailSender;
    public RegisterController(
        UserManager<StuffPackerUser> userManager,
        IUserStore<StuffPackerUser> userStore,
        SignInManager<StuffPackerUser> signInManager,
        ILogger<RegisterController> logger)
    {
        _userManager = userManager;
        _userStore = userStore;

        // _emailStore = GetEmailStore();
        _signInManager = signInManager;
        _logger = logger;

        // _emailSender = emailSender;
    }

    [Route("register")]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> RegisterPostAsync(RegisterViewModel inputModel, string returnUrl = null!)
    {
        returnUrl = returnUrl ?? Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = new StuffPackerUser { UserName = inputModel.Email, Email = inputModel.Email };
            var result = await _userManager.CreateAsync(user, inputModel.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("RegisterConfirmation", new { email = inputModel.Email });
                }

                await _signInManager.SignInAsync(user, false);
                return LocalRedirect(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we got this far, something failed, redisplay form
        return View("Register");
    }
}