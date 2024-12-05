using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;


using HelpPlatform.Infrastructure.Identity;

namespace HelpPlatform.Web.Identity;

public class Login : Endpoint<LoginRequest, LoginResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<Login> _logger;

    public Login(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<Login> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger; 
    }

    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Login an existing user.";
            s.Description = "Authenticates a user with the provided credentials.";
            s.ExampleRequest = new LoginRequest { Email = "user@example.com", Password = "Password123!" };
        });
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            _logger.LogError("Login failed for user {Email}. User not found.", request.Email);
            await SendAsync(new LoginResponse { Message = "Invalid login attempt." }, 401);
            return;
        }
        var valid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (valid)
        {
            var jwtToken = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = "sua-chave-secreta-com-32-caracteres"; // TODO - Configurar chave secreta
                o.ExpireAt = DateTime.UtcNow.AddDays(1);
                o.User.Roles.Add("User", "Admin");
                o.User.Claims.Add(("Email", request.Email));
                o.User["UserId"] = "001"; //indexer based claim setting
            });

            var response = new LoginResponse { Message = "Login successful!",
                                               Email = request.Email ?? string.Empty,
                                               Token = jwtToken};
            await SendAsync(response, 200); 
        }
        else
        {
            _logger.LogError("Login failed for user {Email}", request.Email);

            await SendAsync(new LoginResponse { Message = "Invalid login attempt." }, 401); 
        }
    }
    
}
