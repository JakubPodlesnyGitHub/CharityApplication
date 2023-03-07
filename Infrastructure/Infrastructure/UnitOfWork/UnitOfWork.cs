using Application.Interfaces.UnitOfWork;
using Infrastructure.Contexts;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;

        public UnitOfWork(CharityApplicationDbContext charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
        }

        public TEntity GetRepository<TEntity>() where TEntity : class
        {
            var result = Activator.CreateInstance(typeof(TEntity), _charityApplicationDbContext) as TEntity;
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task Commit()
        {
            await _charityApplicationDbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _charityApplicationDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}