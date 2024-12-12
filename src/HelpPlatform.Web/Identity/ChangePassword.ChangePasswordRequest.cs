using System.ComponentModel.DataAnnotations;

namespace HelpPlatform.Web.Identity;
public class ChangePasswordRequest
{
    public required string CurrentPassword { get; set; }

    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
    public required string NewPassword { get; set; }

    [Compare("NewPassword", ErrorMessage = "As senhas não coincidem.")]
    public required string ConfirmNewPassword { get; set; }
}