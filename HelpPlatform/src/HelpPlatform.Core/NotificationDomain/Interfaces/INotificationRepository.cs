using Ardalis.Result;
 namespace HelpPlatform.Core.NotificationDomain.Interfaces;

public interface INotificationRepository : IRepository<Notification>
{
    Task AddAsync(Notification notification, CancellationToken cancellationToken);
    // Se necessário, adicione métodos específicos para o repositório de notificações aqui
}