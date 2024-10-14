using Ardalis.Specification;

namespace HelpPlatform.Core.ResourceType.ResourceTypeAggregate.Specifications;

public class ResourceTypeByIdSpec : Specification<ResourceType> {
    public ResourceTypeByIdSpec(int resourceTypeId) {
        Query
            .Where(resourceType => resourceType.Id == resourceTypeId);
    }
}