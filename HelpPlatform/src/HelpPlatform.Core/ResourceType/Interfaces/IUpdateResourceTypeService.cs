using Ardalis.Result;

namespace HelpPlatform.Core.ResourceType.Interfaces;

public interface IUpdateResourceTypeService
{
    public Task<Result> UpdateResourceType(int resourceTypeId, string resourceTypeName, CancellationToken cancellationToken);
}