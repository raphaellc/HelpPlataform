using System.Data.Common;

namespace HelpPlatform.UseCases.ResourceTypes;

public class ResourceTypeDTO(int id, string name, string scale)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Scale { get; set; } = scale;
}
