using MongoDB.Bson;
using MongoDB.Driver;
using NotificationService.Data.Models;
using NotificationService.Interfaces;
using static MongoDB.Driver.WriteConcern;

namespace NotificationService.Data.Repositories
{
    public class NotificationRepo : BaseRepo<Notification>, INotificationRepo
    {
        protected readonly IMongoNotificationContext _mongoContext;
        protected IMongoCollection<Notification> _dbCollection;

        public NotificationRepo(IMongoNotificationContext context) : base(context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<Notification>(GetCollectionName());
        }

        public IEnumerable<Notification> GetAbonentNotifications(int abonentId)
        {
            var notifications = _dbCollection.Find(n => n.ReceiverAbonentId == abonentId);
            return notifications.ToList();
        }

        public bool NotificationExists(string notificationId)
        {
            var notification = Get(notificationId); 
            return notification == null ? false : true;
        }

        public void NotificationsMarkViewed(List<string> notificationIds)
        {
            foreach (var id in notificationIds)
            {
                var notification = Get(id);
                notification.IsRead = true;
                Update(notification);
            }
        }

        public void NotificationsMarkAllViewed(int abonentId)
        {
            var filter = Builders<Notification>.Filter.And( 
                Builders<Notification>.Filter.Where(n => n.ReceiverAbonentId == abonentId),
                Builders<Notification>.Filter.Where(n => n.IsRead == false));

            var update = Builders<Notification>.Update.Set(n => n.IsRead, true);
            
            _dbCollection.UpdateMany(filter, update);
        }
    }
}
