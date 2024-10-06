using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace HelpPlatform.Core.ResourceType;

public class ResourceType(string name, int quantity, string scale) : EntityBase, IAggregateRoot
{
    public string Name { get; private set; } = Guard.Against.NullOrWhiteSpace(name,nameof(name));

    public int Quantity { get; private set; } = quantity;

    public string Scale { get; private set; } = Guard.Against.NullOrWhiteSpace(scale,nameof(scale));

    public void UpdateName(string name)
    {
        this.Name = Guard.Against.NullOrWhiteSpace(name,nameof(name));
    }
    public void UpdateScale(string scale)
    {
        this.Scale = Guard.Against.NullOrWhiteSpace(scale,nameof(scale));
    }
    public void UpdateQuantity(int quantity)
    {
        this.Quantity = Guard.Against.Null(quantity,nameof(quantity));
    }
}

