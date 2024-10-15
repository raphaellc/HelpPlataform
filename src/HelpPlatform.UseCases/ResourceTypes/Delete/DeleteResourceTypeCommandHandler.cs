using Ardalis.Result;
using Ardalis.SharedKernel;
using HelpPlatform.Core.ResourceType.Interfaces;

namespace HelpPlatform.UseCases.ResourceTypes.Delete;

public class DeleteUserCommandHandler(IDeleteResourceTypeService _deleteResourceTypeService) : ICommandHandler<DeleteResourceTypeCommand, Result>
{
    public async Task<Result> Handle(DeleteResourceTypeCommand request, CancellationToken cancellationToken)
    {
        return await _deleteResourceTypeService.DeleteResourceType(request.ResourceTypeId, cancellationToken);
    }
}
