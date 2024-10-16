using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.ResourceTypeDomain.Interfaces;

namespace HelpPlatform.UseCases.ResourceTypes.Update;

public class UpdateResourceTypeHandler(IUpdateResourceTypeService _updateResourceTypeService) : ICommandHandler<UpdateResourceTypeCommand, Result<int>>
{
    public async Task<Result<int>> Handle(UpdateResourceTypeCommand request, CancellationToken cancellationToken)
    {
        return await _updateResourceTypeService.UpdateResourceType(request.ResourceTypeId, request.NewName, request.NewScale, cancellationToken);
    }
}
