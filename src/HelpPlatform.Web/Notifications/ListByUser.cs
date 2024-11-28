using FastEndpoints;
using HelpPlatform.UseCases.Notifications.List;
using MediatR;

namespace HelpPlatform.Web.Notifications;

public class ListByUser : Endpoint<ListNotificationsByUserRequest, ListNotificationsByUserResponse>
    {
        private readonly IMediator _mediator;

        public ListByUser(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get(ListNotificationsByUserRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "List Notifications by User ID.";
                s.Description = "List all notifications for a specific user.";
                s.ExampleRequest = new ListNotificationsByUserRequest
                {
                    UserId = 1
                };
            });
        }

        public override async Task HandleAsync(ListNotificationsByUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ListNotificationsQuery(request.UserId!.Value), cancellationToken);
            Console.WriteLine(result);

            if (result.IsSuccess)
            {
                Response = new ListNotificationsByUserResponse(result.Value);
                return;
            }

            foreach (var resultError in result.Errors)
            {
                AddError(resultError);
            }

            ThrowIfAnyErrors();
        }
    }
