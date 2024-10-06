using Ardalis.Result;
using Ardalis.SharedKernel;
using HelpPlatform.Core.ResourceType.Interfaces;

namespace HelpPlatform.Core.ResourceType.Services;

public class UpdateResourceTypeService(IRepository<ResourceType> _repository) : IUpdateResourceTypeService
{
    public async Task<Result> UpdateResourceTypeName(int resourceTypeId, string resourceTypeName, CancellationToken cancellationToken)
    {
        var existingResourceType = await _repository.GetByIdAsync(resourceTypeId);

        if (existingResourceType is null)
        {
            return Result.NotFound();
        }
        existingResourceType.UpdateName(resourceTypeName);

        await _repository.UpdateAsync(existingResourceType, cancellationToken);
        return Result.Success();
    }
    public async Task<Result> UpdateResourceTypeQuantity(int resourceTypeId, int resourceTypeQuantity, CancellationToken cancellationToken)
    {
        var existingResourceType = await _repository.GetByIdAsync(resourceTypeId);

        if (existingResourceType is null)
        {
            return Result.NotFound();
        }
        existingResourceType.UpdateQuantity(resourceTypeQuantity);

        await _repository.UpdateAsync(existingResourceType, cancellationToken);
        return Result.Success();
    }
    public async Task<Result> UpdateResourceTypeScale(int resourceTypeId, string resourceTypeScale, CancellationToken cancellationToken)
    {
        var existingResourceType = await _repository.GetByIdAsync(resourceTypeId);

        if (existingResourceType is null)
        {
            return Result.NotFound();
        }
        existingResourceType.UpdateScale(resourceTypeScale);

        await _repository.UpdateAsync(existingResourceType, cancellationToken);
        return Result.Success();
    }
}