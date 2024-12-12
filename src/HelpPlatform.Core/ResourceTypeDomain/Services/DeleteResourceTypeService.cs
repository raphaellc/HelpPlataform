using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.ResourceTypeDomain.Interfaces;

namespace HelpPlatform.Core.ResourceTypeDomain.Services;

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
