using Ardalis.Result;
using Ardalis.SharedKernel;
using HelpPlatform.Core.ResourceType.Interfaces;

namespace HelpPlatform.Core.ResourceType.Services;

public class UpdateResourceTypeService(IRepository<ResourceType> _repository) : IUpdateResourceTypeService
{
    public async Task<Result> UpdateResourceType(int resourceTypeId, string resourceTypeName, int resourceTypeQuantity, string resourceTypeScale, CancellationToken cancellationToken)
    {
        var existingResourceType = await _repository.GetByIdAsync(resourceTypeId);

        if (existingResourceType is null)
        {
            return Result.NotFound();
        }
        existingResourceType.UpdateName(resourceTypeName);
        existingResourceType.UpdateQuantity(resourceTypeQuantity);
        existingResourceType.UpdateScale(resourceTypeScale);

        await _repository.UpdateAsync(existingResourceType, cancellationToken);
        return Result.Success();
    }
}