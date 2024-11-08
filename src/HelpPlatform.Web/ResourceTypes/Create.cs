using FastEndpoints;
using HelpPlatform.UseCases.ResourceTypes.Create;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.ResourceTypes;

public class Create(IMediator _mediator) : Endpoint<CreateResourceTypeRequest, CreateResourceTypeResponse>
{
    public override void Configure()
    {
        Post(CreateResourceTypeRequest.Route);
        AllowAnonymous();
        Summary(s => {
            s.ExampleRequest = new CreateResourceTypeRequest { Name = "TEST", Scale = "TEST" };
        });
    }
    public override async Task HandleAsync(
        CreateResourceTypeRequest request,
        CancellationToken cancellationToken) 
        {
            var result = await _mediator.Send(new CreateResourceTypeCommand(
            request.Name!,
            request.Scale!),
            cancellationToken);

            await this.SendResponse(result, r => new CreateResourceTypeResponse(result.Value,
                request.Name!,
                request.Scale!));
        }
}
