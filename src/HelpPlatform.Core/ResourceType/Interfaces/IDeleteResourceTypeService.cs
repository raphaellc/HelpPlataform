using Ardalis.Result;

namespace HelpPlatform.Core.ResourceType.Interfaces;

public interface IDeleteResourceTypeService 
{
    public Task<Result> DeleteResourceType(int resourceTypeId, CancellationToken cancellationToken);
}