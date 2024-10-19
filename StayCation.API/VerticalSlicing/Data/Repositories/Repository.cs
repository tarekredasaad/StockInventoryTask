using Microsoft.EntityFrameworkCore;
using StayCation.API.VerticalSlicing.Data.Data;
using StayCation.API.VerticalSlicing.Data.Models;
using System.Linq.Expressions;

namespace StayCation.API.VerticalSlicing.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public async Task<T> First(Expression<Func<T, bool>> predicate)
        {
            return await Get(predicate).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().Where(x => !x.Deleted).AsNoTracking();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            var query = GetAll().Where(predicate);
            
            return query;
        }
        public async Task<IQueryable<T>> GetAllPag(Expression<Func<T, bool>> predicate,
             int take = 10, int skip = 0,
            params Expression<Func<T, object>>[] includes
            )
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }


            query = query.Where(predicate).Skip(skip).Take(take);


            //var result = await query.Skip(skip).Take(take);//.ToListAsync();

            return query;
        }
        public async Task<T> GetByIDAsync(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetWithTrackinByIDAsync(int id)
        {
            return await _context.Set<T>()
                    .Where(x => !x.Deleted && x.Id == id)
                    .AsTracking()
                    .FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

        public void Delete(int id)
        {
            T entity = _context.Find<T>(id);
            Delete(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
