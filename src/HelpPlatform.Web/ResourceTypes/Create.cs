using FastEndpoints;
using HelpPlatform.UseCases.ResourceTypes.Create;
using HelpPlatform.Web.ResourceTypes;
using MediatR;

namespace HelpPlatform.Web.ResourceTypes;

public class Create(IMediator _mediator) : Endpoint<CreateResourceTypeRequest, CreateResourceTypeResponse>
{
    public override void Configure()
    {
        Post(CreateResourceTypeRequest.Route);
        AllowAnonymous();
        Summary(s => {
            // XML Docs are used by default but are overridden by these properties:
            //s.Summary = "Create a new Contributor.";
            //s.Description = "Create a new Contributor. A valid name is required.";
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

            if (result.IsSuccess)
            {
                Response = new CreateResourceTypeResponse(result.Value,
                request.Name!,
                request.Scale!);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
}
