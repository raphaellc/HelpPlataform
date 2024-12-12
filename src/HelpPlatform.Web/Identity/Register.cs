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
/// Register a new user
/// </summary>
/// <remarks>
/// Creates a new user account.
/// </remarks>
public class Register : Endpoint<RegisterRequest, RegisterResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public Register(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public override void Configure()
    {
        Post("/register"); // Define o caminho do endpoint
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Register a new user";
            s.Description = "Creates a new user account. A valid email and password are required.";
            s.ExampleRequest = new RegisterRequest { Email = "user@example.com", Password = "Password123!" };
        });
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            var response = new RegisterResponse { Message = "User registered successfully!" };
            await SendAsync(response, 201); // 201 Created
        }
        else
        {
            var logger = HttpContext.RequestServices.GetRequiredService<ILogger<Register>>();
        
            // Logando os erros
            foreach (var error in result.Errors)
            {
                logger.LogError(error.Description);
            }
        }
    }
}
