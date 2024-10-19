using StayCation.API.VerticalSlicing.Data.Models;
using System.Linq.Expressions;

namespace StayCation.API.VerticalSlicing.Data.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> First(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetByIDAsync(int id);
        Task<IQueryable<T>> GetAllPag(Expression<Func<T, bool>> predicate,
             int take = 10, int skip = 0,
            params Expression<Func<T, object>>[] includes
            );
        Task<T> GetWithTrackinByIDAsync(int id);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        Task SaveChangesAsync();
    }
}
