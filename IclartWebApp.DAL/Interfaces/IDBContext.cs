using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace IclartWebApp.DAL
{
    public interface IDBContext
    {

        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Type GetType();
        int SaveChanges();
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}