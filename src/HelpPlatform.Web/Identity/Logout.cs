using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

using HelpPlatform.Infrastructure.Identity;
namespace HelpPlatform.Web.Identity;

public class Logout : Endpoint<LogoutRequest, LogoutResponse>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<Logout> _logger;

    public Logout(SignInManager<ApplicationUser> signInManager, ILogger<Logout> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/logout");
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Logout an existing user.";
            s.Description = "Logs out the user by clearing session";
        });
    }

    public override async Task HandleAsync(LogoutRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _signInManager.SignOutAsync();

            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            HttpContext.Session.Clear();

            _logger.LogInformation("User logged out successfully.");

            await SendAsync(new LogoutResponse { Message = "Logout Successful."}, 200);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while logging out.");
            await SendAsync(new LogoutResponse { Message = "Logout failed." }, 500);
        }
    }
}
