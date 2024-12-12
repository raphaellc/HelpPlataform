using Ardalis.Result;
using Ardalis.Specification;
using HelpPlatform.SharedKernel;
 
namespace HelpPlatform.Core.NotificationDomain.Interfaces;

public interface INotificationRepository : IRepository<Notification>
{   
   new Task AddAsync(Notification notification,
                      CancellationToken cancellationToken);


}
