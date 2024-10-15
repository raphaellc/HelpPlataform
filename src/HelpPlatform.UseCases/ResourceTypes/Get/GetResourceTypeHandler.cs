using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.ResourceType.ResourceTypeAggregate;
using HelpPlatform.Core.ResourceType.ResourceTypeAggregate.Specifications;
using HelpPlatform.UseCases.ResourceTypes;
using HelpPlatform.UseCases.ResourceTypes.Get;

namespace HelpPlatform.UseCases.ResourceType.Get;

/// <summary>
/// Queries don't necessarily need to use repository methods, but they can if it's convenient
/// </summary>
public class GetResourceTypeHandler(IReadRepository<ResourceType> _repository)
    : IQueryHandler<GetResourceTypeQuery, Result<ResourceTypeDTO>> {
    public async Task<Result<ResourceTypeDTO>> Handle(GetResourceTypeQuery request, CancellationToken cancellationToken) {
        var spec = new ResourceTypeByIdSpec(request.ResourceTypeId);
        var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (entity == null) return Result.NotFound();

        return new ResourceTypeDTO(entity.Id, entity.Name, entity.Quantity , entity.Scale);
    }
}
