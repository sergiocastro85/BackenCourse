namespace WebApplicationBackend.Repository
{
    public interface IRepository<IEntity>
    {
        Task<IEnumerable<IEntity>> GetAsync();

        Task<IEntity> GetById(int id);

        Task Add(IEntity entity);

        void UpdateAsync(IEntity entity);

        void DeleteAsync(IEntity entity);

        Task Save();

        IEnumerable<IEntity> Search(Func<IEntity, bool> filter);
    }
}
