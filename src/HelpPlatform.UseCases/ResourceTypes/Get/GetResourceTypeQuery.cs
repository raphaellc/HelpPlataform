using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.ResourceTypes.Get;

public record GetResourceTypeQuery(int ResourceTypeId) : IQuery<Result<ResourceTypeDTO>>;
