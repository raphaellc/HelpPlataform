
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FastEndpoints;

namespace HelpPlatform.Web.Identity;

public class ResetPasswordResponse
{
    public required string Message { get; set; }
}