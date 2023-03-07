namespace Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public TEntity GetRepository<TEntity>() where TEntity : class;

        public Task Commit();
    }
}