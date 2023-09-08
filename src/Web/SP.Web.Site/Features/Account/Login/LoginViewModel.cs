using System.ComponentModel.DataAnnotations;

namespace SP.Web.Site.Features.Account.Login;

public class LoginViewModel
{
    [Microsoft.Build.Framework.Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "RememberMe")]
    public bool RememberMe { get; set; }

    public string ReturnUrl { get; set; } = string.Empty;
}