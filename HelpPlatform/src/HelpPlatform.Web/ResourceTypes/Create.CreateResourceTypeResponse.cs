namespace HelpPlatform.Web.ResourceTypes;

public class CreateResourceTypeResponse(int id, string name, int quantity,  string scale)
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = name;

    public int Quantity { get; set; } = quantity;

    public string? Scale { get; set; } = scale;
}
