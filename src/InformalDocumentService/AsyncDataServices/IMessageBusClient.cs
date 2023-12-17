using InformalDocumentService.Dtos;

namespace InformalDocumentService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void CreateNotification(NotificationCreateDto notificationCreateDto);
    }
}
