namespace NotificationService.Interfaces
{
    public interface IBaseRepo<TEntity> where TEntity : class
    {
        void Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(string id);
        TEntity Get(string id);
    }
}
