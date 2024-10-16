using System.ComponentModel.DataAnnotations;

namespace HelpPlatform.Web.ResourceTypes;

public class CreateResourceTypeRequest
{
    public const string Route = "/ResourceTypes";
    
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Scale { get; set; }
}
