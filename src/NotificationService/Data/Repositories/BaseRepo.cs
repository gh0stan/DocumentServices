using MongoDB.Bson;
using MongoDB.Driver;
using NotificationService.Data.Models;
using NotificationService.Interfaces;
using System.Collections.Generic;

namespace NotificationService.Data.Repositories
{
    public abstract class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : EntityBase
    {
        protected readonly IMongoNotificationContext _mongoContext;
        protected IMongoCollection<TEntity> _dbCollection;

        protected BaseRepo(IMongoNotificationContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>(GetCollectionName());
        }

        protected static string GetCollectionName()
        {
            return (typeof(TEntity).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
                as BsonCollectionAttribute).CollectionName;
        }

        public void Create(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }
             _dbCollection.InsertOne(obj);
        }

        public void Delete(string id)
        {
            _dbCollection.DeleteOne(Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id)));
        }

        public TEntity Get(string id)
        {
            var data = _dbCollection.Find(Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id)));
            return data.SingleOrDefault();
        }

        public void Update(TEntity obj)
        {
            _dbCollection.ReplaceOne(Builders<TEntity>.Filter.Eq("_id", obj.Id), obj);
        }
    }
}
