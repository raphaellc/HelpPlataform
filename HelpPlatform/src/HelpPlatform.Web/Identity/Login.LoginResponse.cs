
using System.ComponentModel.DataAnnotations;

namespace HelpPlatform.Web.Identity;
public class LoginResponse{
    public string Message { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}

