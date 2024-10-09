using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HelpPlatform.UseCases.ResourceTypes.Get;

public record GetResourceTypeQuery(int ResourceTypeId) : IQuery<Result<ResourceTypeDTO>>;