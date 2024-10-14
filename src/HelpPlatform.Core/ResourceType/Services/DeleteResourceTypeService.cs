using Ardalis.Result;
using Ardalis.SharedKernel;
using HelpPlatform.Core.ResourceType.Interfaces;

namespace HelpPlatform.Core.ResourceType.Services;

public class DeleteResourceTypeService(IRepository<ResourceType> _repository) : IDeleteResourceTypeService
{
    public async Task<Result> DeleteResourceType(int resourceTypeId, CancellationToken cancellationToken)
    {
        var resourceTypeToDelete = await _repository.GetByIdAsync(resourceTypeId);

        if (resourceTypeToDelete is null)
        {
            return Result.NotFound();
        }

        await _repository.DeleteAsync(resourceTypeToDelete);
        return Result.Success();
    }
}