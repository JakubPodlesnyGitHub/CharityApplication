using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        Task<List<TEntity>> GetAllBySpecificData(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> GetById(object id);

        Task<TEntity> GetById(object id1, object id2);

        Task<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Insert(TEntity newObject);

        Task<TEntity> Update(TEntity objectToUpdate);

        Task<TEntity> Delete(object id);

        Task<TEntity> Delete(object id1, object id2);
    }
}