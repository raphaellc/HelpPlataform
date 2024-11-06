
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FastEndpoints;

namespace HelpPlatform.Web.Identity;
public class ForgotPasswordRequest{
        public required string Email { get; set; }
}

