
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FastEndpoints;

namespace HelpPlatform.Web.Identity;

public class ResetPasswordRequest
{
    public required string Token { get; set; }
    public required string Email { get; set; }
    public required string NewPassword { get; set; }
}