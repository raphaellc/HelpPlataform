using FastEndpoints;
using HelpPlatform.Web.Users;

namespace HelpPlatform.Web.AuthTest;

public class AuthTest() : EndpointWithoutRequest<ListAuthTestResponse> {
    public override void Configure() {
        Get("/AuthTest");
        Roles("Admin", "Manager");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken) {
        
        var users = new List<AuthTestResponse>
        {
            new AuthTestResponse(1, "João Silva", "joao.silva@example.com"),
            new AuthTestResponse(2, "Maria Oliveira", "maria.oliveira@example.com"),
            new AuthTestResponse(3, "Carlos Souza", "carlos.souza@example.com"),
            new AuthTestResponse(4, "Ana Costa", "ana.costa@example.com"),
            new AuthTestResponse(5, "Pedro Santos", "pedro.santos@example.com")
        };

        var response = new ListAuthTestResponse
        {
            Users = users
        };

        await SendAsync(response, 200);
    
    }
}
