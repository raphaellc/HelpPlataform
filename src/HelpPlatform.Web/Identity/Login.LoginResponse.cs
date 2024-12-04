
using System.ComponentModel.DataAnnotations;

namespace HelpPlatform.Web.Identity;
public class LoginResponse{
    public string Message { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

