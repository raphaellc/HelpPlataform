using Ardalis.Result;

namespace HelpPlatform.Core.ResourceTypeDomain.Interfaces;

public interface IDeleteResourceTypeService 
{
    public Task<Result> DeleteResourceType(int resourceTypeId, CancellationToken cancellationToken);
}