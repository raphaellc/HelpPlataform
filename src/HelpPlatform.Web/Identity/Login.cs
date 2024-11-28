using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Text;

using HelpPlatform.Infrastructure.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user == null)
            {
                _logger.LogError("User not found after successful login for email {Email}", request.Email);
                await SendAsync(new LoginResponse { Message = "User not found." }, 404); // 404 Not Found
                return;
            }

             var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty), // Usando string.Empty se user.Id for nulo
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty) // Usando string.Empty se user.Email for nulo
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sua-chave-secreta-com-32-caracteres")); // TODO - Insira uma chave secreta segura
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                issuer: "seu_emissor",  // TODO-Altere para seu emissor
                audience: "sua_audiencia",  // TODO-Altere para sua audiência
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new LoginResponse { Message = "Login successful!",
                                               UserId = user.Id ?? string.Empty,
                                               Token = tokenString};
            await SendAsync(response, 200); 
        }
        else
        {
            _logger.LogError("Login failed for user {Email}", request.Email);

            await SendAsync(new LoginResponse { Message = "Invalid login attempt." }, 401); 
        }
    }
    
}
