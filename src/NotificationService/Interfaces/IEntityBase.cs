using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NotificationService.Interfaces
{
    public interface IEntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedOn { get; }
    }
}
