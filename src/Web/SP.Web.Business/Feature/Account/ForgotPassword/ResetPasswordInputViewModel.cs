using System.ComponentModel.DataAnnotations;

namespace SP.Web.Business.Feature.Account.ForgotPassword;

public class ResetPasswordInputViewModel
{
    public string Code { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}