using Ardalis.Result;
using HelpPlatform.UseCases.ResourceTypes.Get;
using HelpPlatform.UseCases.ResourceTypes.Update;
using FastEndpoints;
using MediatR;

namespace HelpPlatform.Web.ResourceTypes;

public class Update(IMediator _mediator)
    : Endpoint<UpdateResourceTypeRequest, UpdateResourceTypeResponse> {
    public override void Configure() {
        Put(UpdateResourceTypeRequest.Route);
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(
        UpdateResourceTypeRequest request,
        CancellationToken cancellationToken) {
        var result = await _mediator.Send(new UpdateResourceTypeCommand(request.Id, request.Name!, request.Scale!), cancellationToken);

        if (result.Status == ResultStatus.NotFound){
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        var query = new GetResourceTypeQuery(request.ResourceTypeId);

        var queryResult = await _mediator.Send(query, cancellationToken);

        if (queryResult.Status == ResultStatus.NotFound){
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (queryResult.IsSuccess){
            var dto = queryResult.Value;
            Response = new UpdateResourceTypeResponse(new ResourceTypeRecord(dto.Id, dto.Name, dto.Scale));
            return;
        }
    }
}
