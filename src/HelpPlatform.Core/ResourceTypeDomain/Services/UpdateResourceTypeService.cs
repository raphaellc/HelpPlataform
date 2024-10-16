using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.ResourceTypeDomain.Interfaces;

namespace HelpPlatform.Core.ResourceTypeDomain.Services;

public class UpdateResourceTypeService(IRepository<ResourceType> _repository) : IUpdateResourceTypeService
{
    public async Task<Result> UpdateResourceType(int resourceTypeId, string resourceTypeName, string resourceTypeScale, CancellationToken cancellationToken)
    {
        var existingResourceType = await _repository.GetByIdAsync(resourceTypeId);

        if (existingResourceType is null)
        {
            return Result.NotFound();
        }
        existingResourceType.UpdateName(resourceTypeName);
        existingResourceType.UpdateScale(resourceTypeScale);

        await _repository.UpdateAsync(existingResourceType, cancellationToken);
        return Result.Success();
    }
}
