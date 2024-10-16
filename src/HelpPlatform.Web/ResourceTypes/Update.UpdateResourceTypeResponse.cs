namespace HelpPlatform.Web.ResourceTypes;

public class UpdateResourceTypeResponse(ResourceTypeRecord resourceType)
{
    public ResourceTypeRecord ResourceType { get; set; } = resourceType;
}