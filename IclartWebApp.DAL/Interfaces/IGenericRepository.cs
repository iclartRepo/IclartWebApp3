using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);
        void SoftDelete(TEntity entity);
        void HardDelete(TEntity entity);
        void Update(TEntity entity);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
    }
}
