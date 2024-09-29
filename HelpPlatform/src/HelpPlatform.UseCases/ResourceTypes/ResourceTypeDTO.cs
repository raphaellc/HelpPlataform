using System.Data.Common;

namespace HelpPlatform.UseCases.ResourceTypes;

public class ResourceTypeDTO(int id, string name, int quantity, string scale)
{
    int Id { get; set; } = id;
    string Name { get; set; } = name;
    int quantity { get; set; } = quantity;
    string scale { get; set; } = scale;
}