using Ardalis.Specification;

namespace HelpPlatform.Core.ResourceTypeDomain.ResourceTypeAggregate.Specifications;

public class ResourceTypeByIdSpec : Specification<ResourceType> {
    public ResourceTypeByIdSpec(int resourceTypeId) {
        Query
            .Where(resourceType => resourceType.Id == resourceTypeId);
    }
}