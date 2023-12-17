using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NotificationService.Interfaces;

namespace NotificationService.Data.Models
{
    public abstract class EntityBase : IEntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement(elementName: "createdOn")]
        public DateTime CreatedOn => Id.CreationTime;
    }
}
