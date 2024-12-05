using Ardalis.Result;
using HelpPlatform.UseCases.ResourceTypes;
using HelpPlatform.UseCases.ResourceTypes.List;
using FastEndpoints;
using MediatR;

namespace HelpPlatform.Web.ResourceTypes;

public class List(IMediator _mediator) : EndpointWithoutRequest<ResourceTypeListResponse> {
    public override void Configure() {
        Get("/ResourceTypes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken) {
        Result<IEnumerable<ResourceTypeDTO>> result = await _mediator.Send(new ListResourceTypesQuery(null, null), cancellationToken);

        if (result.IsSuccess){
            Response = new ResourceTypeListResponse
            {
                ResourceTypes = result.Value.Select(c => new ResourceTypeRecord(
                Id : c.Id, 
                Name: c.Name, 
                Scale: c.Scale)).ToList()
            };
        }
    }
}