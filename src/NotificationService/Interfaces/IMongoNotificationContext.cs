using MongoDB.Driver;
using NotificationService.Data.Models;

namespace NotificationService.Interfaces
{
    public interface IMongoNotificationContext
    {
        IMongoCollection<Notification> GetCollection<Notification>(string name);
    }
}
