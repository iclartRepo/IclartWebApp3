using IclartWebApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DBContext context;
        private DbSet<TEntity> _dbSet;

        public GenericRepository()
        {
            _dbSet = context.Set<TEntity>();
        }
        public GenericRepository(DBContext contextInitial)
        {
            context = contextInitial;
            _dbSet = context.Set<TEntity>();
        }

        public DBContext GetContext()
        {
            return context;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy?.Invoke(query).ToList() ?? query.ToList();
        }

        public void HardDelete(TEntity entity)
        {
            _dbSet.Remove(entity);
            context.SaveChanges();
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            context.SaveChanges();
        }

        public void SoftDelete(TEntity entity)
        {
            _dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
