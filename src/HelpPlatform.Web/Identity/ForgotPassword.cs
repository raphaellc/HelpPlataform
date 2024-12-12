using Microsoft.AspNetCore.Identity;
using HelpPlatform.Infrastructure.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using FastEndpoints;

namespace HelpPlatform.Web.Identity;

public class ForgotPassword : Endpoint<ForgotPasswordRequest, ForgotPasswordResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ForgotPassword> _logger;
    private readonly IEmailSender<ApplicationUser> _emailSender;

    public ForgotPassword(UserManager<ApplicationUser> userManager, ILogger<ForgotPassword> logger, IEmailSender<ApplicationUser> emailSender)
    {
        _userManager = userManager;
        _logger = logger;
        _emailSender = emailSender;
    }

    public override void Configure()
    {
        Post("/forgot-password");
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Solicitação de redefinição de senha";
            s.Description = "Envia um link de redefinição de senha para o e-mail fornecido.";
            s.ExampleRequest = new ForgotPasswordRequest { Email = "user@example.com" };
        });
    }

    public override async Task HandleAsync(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        // Buscar o usuário pelo e-mail
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            _logger.LogWarning("Usuário com o e-mail {Email} não encontrado.", request.Email);
            await SendAsync(new ForgotPasswordResponse { Message = "Se o e-mail fornecido existir, um link de redefinição de senha será enviado." }, 200);
            return;
        }

        // Gerar o token de redefinição de senha
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        // Criar o link para redefinir a senha
        var resetLink = $"https://seu-dominio.com/reset-password?token={token}&email={request.Email}"; //TODO pagina de reset
        
        // Enviar o e-mail com o link de redefinição de senha
        await _emailSender.SendPasswordResetLinkAsync(user, request.Email, resetLink);

        _logger.LogInformation("Link de redefinição de senha enviado para o e-mail {Email}.", request.Email);

        // Resposta
        await SendAsync(new ForgotPasswordResponse { Message = "Se o e-mail fornecido existir, um link de redefinição de senha será enviado." }, 200);
    }
}
