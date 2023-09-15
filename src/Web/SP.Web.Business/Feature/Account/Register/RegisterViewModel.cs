using System.ComponentModel.DataAnnotations;

namespace SP.Web.Business.Feature.Account.Register;

public class RegisterViewModel
{
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    public string ReturnUrl { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

    public string ConfirmPassword { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}