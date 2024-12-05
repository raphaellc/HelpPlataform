using FastEndpoints;
using Microsoft.AspNetCore.Identity;

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
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<Login> _logger;
    private static string DEFAULT_ROLE = "User";

    public Register(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<Login> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger; 
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
                var roleExists = await _roleManager.RoleExistsAsync(DEFAULT_ROLE);
                if (!roleExists)
                {
                    var role = new IdentityRole(DEFAULT_ROLE);
                    await _roleManager.CreateAsync(role);
                }

                var addRoleResult = await _userManager.AddToRoleAsync(user, DEFAULT_ROLE);

                if (addRoleResult.Succeeded)
                {
                    _logger.LogInformation("User {Email} registered and assigned to role '{DEFAULT_ROLE}'.", request.Email, DEFAULT_ROLE);

                    var response = new RegisterResponse { Message = "User registered successfully!" };

                    await SendAsync(response, 201); // 201 Created
                } 
                else
                {
                    _logger.LogError("Failed to assign role '{DEFAULT_ROLE}' to user {Email}.", DEFAULT_ROLE, request.Email);

                    var response = new RegisterResponse
                    {
                        Message = "User registration successful, but failed to assign role 'usuario'.",
                    };

                    await SendAsync(response, 400); // 400 Bad Request
                }
        }
        else
        {
            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }
        }
    }
}
