using Ardalis.GuardClauses;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.ResourceTypeDomain;

public class ResourceType(string name, string scale) : EntityBase, IAggregateRoot
{
    public string Name { get; private set; } = Guard.Against.NullOrWhiteSpace(name,nameof(name));

    public string Scale { get; private set; } = Guard.Against.NullOrWhiteSpace(scale,nameof(scale));

    public void UpdateName(string name)
    {
        this.Name = Guard.Against.NullOrWhiteSpace(name,nameof(name));
    }
    public void UpdateScale(string scale)
    {
        this.Scale = Guard.Against.NullOrWhiteSpace(scale,nameof(scale));
    }
}
