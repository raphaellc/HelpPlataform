using Ardalis.Result;
using Ardalis.SharedKernel;

namespace HelpPlatform.UseCases.ResourceTypes.List;

public record ListResourceTypesQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<ResourceTypeDTO>>>;