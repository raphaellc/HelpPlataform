using Microsoft.AspNetCore.Identity;
using HelpPlatform.Infrastructure.Identity;
using FastEndpoints;

namespace HelpPlatform.Web.Identity;
public class ResetPassword : Endpoint<ResetPasswordRequest, ResetPasswordResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ResetPassword> _logger;

    public ResetPassword(UserManager<ApplicationUser> userManager, ILogger<ResetPassword> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/reset-password");
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Redefinir senha";
            s.Description = "Permite que o usuário redefina sua senha usando o token enviado por e-mail.";
            s.ExampleRequest = new ResetPasswordRequest
            {
                Token = "token-aqui",
                Email = "user@example.com",
                NewPassword = "NovaSenha123!"
            };
        });
    }

    public override async Task HandleAsync(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        // Validar o token de redefinição de senha
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            _logger.LogError("Usuário não encontrado para o e-mail {Email}.", request.Email);
            await SendAsync(new ResetPasswordResponse { Message = "O usuário não foi encontrado." }, 404);
            return;
        }

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
        if (result.Succeeded)
        {
            _logger.LogInformation("Senha redefinida com sucesso para o usuário {Email}.", request.Email);
            await SendAsync(new ResetPasswordResponse { Message = "Senha redefinida com sucesso!" }, 200);
        }
        else
        {
            _logger.LogError("Erro ao redefinir a senha para o usuário {Email}.", request.Email);
            await SendAsync(new ResetPasswordResponse { Message = "Erro ao redefinir a senha. Verifique o token e tente novamente." }, 400);
        }
    }
}
