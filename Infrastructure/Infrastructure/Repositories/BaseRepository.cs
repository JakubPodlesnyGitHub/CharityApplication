using Application.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;
        private DbSet<TEntity> _dbSet;

        public BaseRepository(CharityApplicationDbContext charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
            _dbSet = charityApplicationDbContext.Set<TEntity>();
        }

        public async Task<TEntity> Delete(object id)
        {
            TEntity? objectToRemove = await _dbSet.FindAsync(id);
            if (objectToRemove != null)
            {
                _charityApplicationDbContext.Attach(objectToRemove);
                _charityApplicationDbContext.Entry(objectToRemove).State = EntityState.Deleted;
            }
            return objectToRemove;
        }

        public async Task<TEntity> Delete(object id1, object id2)
        {
            TEntity? objectToRemove = await _dbSet.FindAsync(id1, id2);
            if (objectToRemove != null)
            {
                _charityApplicationDbContext.Attach(objectToRemove);
                _charityApplicationDbContext.Entry(objectToRemove).State = EntityState.Deleted;
            }
            return objectToRemove;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(object id)
        {
            TEntity? entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task<TEntity> GetById(object id1, object id2)
        {
            TEntity? entity = await _dbSet.FindAsync(id1, id2);
            return entity;
        }

        public async Task<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity? entity = await _dbSet.Where(predicate).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<List<TEntity>> GetAllBySpecificData(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<TEntity> Insert(TEntity newObject)
        {
            _charityApplicationDbContext.Attach(newObject);
            _charityApplicationDbContext.Entry(newObject).State = EntityState.Added;
            return newObject;
        }

        public async Task<TEntity> Update(TEntity objectToUpdate)
        {
            _charityApplicationDbContext.Attach(objectToUpdate);
            _charityApplicationDbContext.Entry(objectToUpdate).State = EntityState.Modified;
            return objectToUpdate;
        }
    }
}