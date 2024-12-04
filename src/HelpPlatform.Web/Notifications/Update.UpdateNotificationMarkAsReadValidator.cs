using HelpPlatform.Infrastructure.Data.Config;
using FastEndpoints;
using FluentValidation;

namespace HelpPlatform.Web.Notifications;

public class UpdateNotificationAsReadValidator : Validator<UpdateNotificationAsReadRequest>
{
    public UpdateNotificationAsReadValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty()
            .WithMessage("Id is required");
        
        RuleFor(request => request.UserId)
            .NotEmpty()
            .WithMessage("userId is required");
            
        RuleFor(request => request.NotificationId)
            .Must((args, resourceTypeId) => args.Id == resourceTypeId)
            .WithMessage("Route and body Ids must match; cannot update Id of an existing Notification.");
    }
}