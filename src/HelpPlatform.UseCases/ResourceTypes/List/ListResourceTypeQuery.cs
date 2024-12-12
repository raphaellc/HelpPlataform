using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.ResourceTypes.List;

public record ListResourceTypesQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<ResourceTypeDTO>>>;
