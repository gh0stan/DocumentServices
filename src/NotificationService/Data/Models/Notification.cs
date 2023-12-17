using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace NotificationService.Data.Models
{
    [BsonCollection("notifications")]
    public class Notification : EntityBase
    {
        [BsonElement(elementName: "receiverAbonentId")]
        public int ReceiverAbonentId { get; set; }

        [BsonElement(elementName: "message")]
        public string Message { get; set; }

        [BsonElement(elementName: "isRead")]
        public bool IsRead { get; set; }
    }
}
