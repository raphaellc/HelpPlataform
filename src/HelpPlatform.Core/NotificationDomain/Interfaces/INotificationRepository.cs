using Ardalis.Result;
using HelpPlatform.SharedKernel;
 
namespace HelpPlatform.Core.NotificationDomain.Interfaces;

public interface INotificationRepository : IRepository<Notification>
{   
   new Task AddAsync(Notification notification,
                      CancellationToken cancellationToken);
    // Se necessário, adicione métodos específicos para o repositório de notificações aqui
}
