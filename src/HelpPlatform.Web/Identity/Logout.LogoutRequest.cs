
using System.ComponentModel.DataAnnotations;
using FastEndpoints;

namespace HelpPlatform.Web.Identity;
public class LogoutRequest{
    public const string Route = "/Identity";
    public bool IsLoggedOut { get; set; }
}

