using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotificationService.Interfaces;

namespace NotificationService.Data
{
    public class MongoNotificationContext : IMongoNotificationContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoNotificationContext(IConfiguration configuration)
        {
            _mongoClient = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _db = _mongoClient.GetDatabase(configuration["DatabaseName"]);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

    }
}
