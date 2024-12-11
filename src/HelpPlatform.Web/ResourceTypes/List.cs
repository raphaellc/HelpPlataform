using Ardalis.Result;
using HelpPlatform.UseCases.ResourceTypes;
using HelpPlatform.UseCases.ResourceTypes.List;
using FastEndpoints;
using MediatR;
using HelpPlatform.Web.Extensions;

namespace HelpPlatform.Web.ResourceTypes;

public class List(IMediator _mediator) : EndpointWithoutRequest<ResourceTypeListResponse> {
    public override void Configure() {
        Get("/ResourceTypes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken) {
        Result<IEnumerable<ResourceTypeDTO>> result = await _mediator.Send(new ListResourceTypesQuery(null, null), cancellationToken);

        await this.SendResponse(result, r => new ResourceTypeListResponse {
            ResourceTypes = r.Value.Select(resourceType => new ResourceTypeRecord(
                resourceType.Id,
                resourceType.Name,
                resourceType.Scale)).ToList()
        });
        
    }
}