using Ardalis.Result;

namespace HelpPlatform.Core.ResourceTypeDomain.Interfaces;

public interface IUpdateResourceTypeService
{
    public Task<Result> UpdateResourceType(int resourceTypeId, string resourceTypeName, string resourceTypeScale, CancellationToken cancellationToken);
}
