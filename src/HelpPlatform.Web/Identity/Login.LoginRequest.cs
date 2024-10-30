
using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace HelpPlatform.Web.Identity;
public class LoginRequest{
    public const string Route = "/Identity";
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [QueryParam]
    public bool UseCookies { get; set; } = true; 
    [QueryParam]
    public bool UseSessionCookies { get; set; } = true; 
}

