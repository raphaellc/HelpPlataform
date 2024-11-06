using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using HelpPlatform.Infrastructure.Identity;

namespace HelpPlatform.Web.Identity;
public class ChangePassword : Endpoint<ChangePasswordRequest, ChangePasswordResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ChangePassword(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public override void Configure()
    {
        Put("/change-password");
        AllowAnonymous(); // Ou autenticar conforme necessário
        Summary(s =>
        {
            s.Summary = "Alterar senha do usuário";
            s.Description = "Permite que o usuário altere sua senha, fornecendo a senha atual, nova senha e confirmação da nova senha.";
            s.ExampleRequest = new ChangePasswordRequest { CurrentPassword = "Password123!", NewPassword = "Teste123!", ConfirmNewPassword = "Teste123!" };
        });
    }

    public override async Task HandleAsync(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        // Recuperar o usuário com base no email ou ID
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null)
        {
            await SendAsync(new ChangePasswordResponse { Message = "Usuário não encontrado." }, 404);
            return;
        }

        // Validar a senha atual
        var result = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
        if (!result)
        {
            await SendAsync(new ChangePasswordResponse { Message = "Senha atual incorreta." }, 400);
            return;
        }

        // Alterar a senha do usuário
        var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (changePasswordResult.Succeeded)
        {
            await SendAsync(new ChangePasswordResponse { Message = "Senha alterada com sucesso!" }, 200);
        }
        else
        {
            var errors = string.Join(", ", changePasswordResult.Errors.Select(e => e.Description));
            await SendAsync(new ChangePasswordResponse { Message = $"Erro ao alterar a senha: {errors}" }, 400);
        }
    }
}