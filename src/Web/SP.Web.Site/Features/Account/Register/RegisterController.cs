using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Business.Feature.Account.Register;
using SP.Web.Business.Feature.User.CreateUser;
using SP.Web.Site.Model;

namespace SP.Web.Site.Features.Account.Register;

[Route("account")]
public class RegisterController : Controller
{
    private readonly SignInManager<StuffPackerUser> _signInManager;
    private readonly UserManager<StuffPackerUser> _userManager;
    private readonly IUserStore<StuffPackerUser> _userStore;
    private readonly IMediator _mediator;

    // private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly ILogger<RegisterController> _logger;

    // private readonly IEmailSender _emailSender;
    public RegisterController(
        UserManager<StuffPackerUser> userManager,
        IUserStore<StuffPackerUser> userStore,
        SignInManager<StuffPackerUser> signInManager,
        ILogger<RegisterController> logger,
        IMediator mediator)
    {
        _userManager = userManager;
        _userStore = userStore;

        // _emailStore = GetEmailStore();
        _signInManager = signInManager;
        _logger = logger;
        _mediator = mediator;

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

                try
                {
                    var user2 = await _userManager.FindByEmailAsync(inputModel.Email);
                    await _mediator.Send(new CreateUserCommand(Guid.Parse(user2!.Id), Guid.Parse(user2!.Id)));
                }
                catch (Exception e)
                {
                    _logger.LogError(e.ToString());
                }

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