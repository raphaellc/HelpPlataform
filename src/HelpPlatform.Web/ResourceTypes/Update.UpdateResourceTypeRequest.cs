using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace HelpPlatform.Web.ResourceTypes;

public class UpdateResourceTypeRequest 
{
    public const string Route = "/ResourceTypes/{ResourceTypeId:int}";

    public static string BuildRoute(int ResourceTypeId) => Route.Replace("{ResourceTypeId:int}", ResourceTypeId.ToString());

    public int ResourceTypeId { get; set; }
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Scale { get; set; }

}
