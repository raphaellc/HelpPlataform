namespace HelpPlatform.Web.ResourceTypes;

public class CreateResourceTypeResponse(int id, string name, string scale)
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = name;

    public string? Scale { get; set; } = scale;
}
