using Ardalis.Result;

namespace HelpPlatform.Core.ResourceType.Interfaces;

public interface IUpdateResourceTypeService
{
    public Task<Result> UpdateResourceTypeName(int resourceTypeId, string resourceTypeName, CancellationToken cancellationToken);
    public Task<Result> UpdateResourceTypeQuantity(int resourceTypeId, int resourceTypeQuantity, CancellationToken cancellationToken);
    public Task<Result> UpdateResourceTypeScale(int resourceTypeId, string resourceTypeScale, CancellationToken cancellationToken);
}