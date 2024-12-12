using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace HelpPlatform.Web.Notifications;

public class UnreadNotificationsByUserRequest
{
    public const string Route = "/notifications/unread/{UserId}";
    [FromRoute] public int UserId { get; set; }
}
