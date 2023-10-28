using System.Text;
using System.Text.Encodings.Web;
using Azure.Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SP.Web.Business.Feature.Account.ForgotPassword;
using SP.Web.Site.Model;

namespace SP.Web.Site.Features.Account.ForgotPassword;

[Route("account")]
public class ForgotPasswordController : Controller
{
    private readonly UserManager<StuffPackerUser> _userManager;
    private readonly IEmailSender _emailSender;

    public ForgotPasswordController(UserManager<StuffPackerUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [Route("ForgotPassword")]
    public IActionResult ForgotPassword()
    {
        return View("ForgotPassword", new ForgotPasswordViewModel());
    }

    [Route("ResetPasswordConfirmation")]
    public IActionResult ResetPasswordConfirmation()
    {
        return View("ResetPasswordConfirmation");
    }

    [Route("ResetPassword")]
    public IActionResult ResetPassword([FromQuery] string code)
    {
        if (code == string.Empty || code == null)
        {
            return BadRequest("A code must be supplied for password reset.");
        }
        else
        {
            var model = new ResetPasswordInputViewModel
            {
                Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
            };
            return View("ResetPassword", model);
        }
    }

    [Route("ForgotPassword")]
    [HttpPost]
    public async Task<IActionResult> RegisterPostAsync(ForgotPasswordViewModel input, string returnUrl = null!)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            // if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = "https://" + Request.Host + "/Account/ResetPassword?code=" + code;

            // var callbackUrl = Url.Page(
            //     "/Account/ResetPassword",
            //     pageHandler: null,
            //     values: new { code },
            //     protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                input.Email,
                "Reset Password",
                $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl!)}'>clicking here</a>.");

            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        return View("ForgotPassword", new ForgotPasswordViewModel());
    }

    [Route("ResetPassword")]
    [HttpPost]
    public async Task<IActionResult> ResetPasswordPostAsync(ResetPasswordInputViewModel input)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = await _userManager.FindByEmailAsync(input.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return RedirectToPage("./ResetPasswordConfirmation");
        }

        var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(input.Code));
        var result = await _userManager.ResetPasswordAsync(user, code, input.Password);
        if (result.Succeeded)
        {
            return RedirectToPage("/ResetPasswordConfirmation");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return RedirectToPage("/");
    }
}