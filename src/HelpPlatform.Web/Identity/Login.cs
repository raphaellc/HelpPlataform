using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using HelpPlatform.Infrastructure.Identity;

namespace HelpPlatform.Web.Identity;

/// <summary>
/// Login an existing user
/// </summary>
/// <remarks>
/// Authenticates a user with the provided credentials.
/// </remarks>
public class Login : Endpoint<LoginRequest, LoginResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public Login(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Login an existing user.";
            s.Description = "Authenticates a user with the provided credentials.";
            s.ExampleRequest = new LoginRequest { Email = "user@example.com", Password = "password123" };
        });
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            // Retrieve user details if necessary
            var user = await _userManager.FindByEmailAsync(request.Email);
            var response = new LoginResponse { Message = "Login successful!", UserId = "teste" };

            // Optionally, add claims to the response or a JWT token generation here

            await SendAsync(response, 200); // 200 OK
        }
        else
        {
            var logger = HttpContext.RequestServices.GetRequiredService<ILogger<Login>>();

            logger.LogError("Login failed for user {Email}", request.Email);

            await SendAsync(new LoginResponse { Message = "Invalid login attempt." }, 401); 
        }
    }
    
}
