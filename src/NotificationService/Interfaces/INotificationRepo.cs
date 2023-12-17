using NotificationService.Data.Models;

namespace NotificationService.Interfaces
{
    public interface INotificationRepo : IBaseRepo<Notification>
    {
        void NotificationsMarkViewed(List<string> notificationIds);
        bool NotificationExists(string notificationId);
        IEnumerable<Notification> GetAbonentNotifications(int abonentId);
        void NotificationsMarkAllViewed(int abonentId);
    }
}
