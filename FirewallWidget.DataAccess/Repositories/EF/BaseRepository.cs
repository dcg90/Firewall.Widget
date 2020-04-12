using FirewallWidget.DataAccess.Contexts;
using FirewallWidget.DataAccess.Contracts;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace FirewallWidget.DataAccess.Repositories.EF
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly EFDbContext dbContext;
        private readonly DbSet<TEntity> entities;

        public BaseRepository(EFDbContext dbContext, DbSet<TEntity> entities)
        {
            this.dbContext = dbContext;
            this.entities = entities;
        }

        public virtual TEntity Create(TEntity entity)
        {
            var e = entities.Add(entity);
            dbContext.SaveChanges();

            return e;
        }

        public virtual TKey Delete(TKey primaryKey)
        {
            var e = entities.Find(primaryKey);
            if (e != null)
            {
                entities.Remove(e);
                dbContext.SaveChanges();
                return primaryKey;
            }
            return default;
        }

        public virtual TEntity Read(TKey primaryKey)
        {
            return entities.Find(primaryKey);
        }

        public virtual IEnumerable<TEntity> Read(
            Expression<Func<TEntity, bool>> predicate)
        {
            return entities.Where(predicate);
        }

        public virtual TEntity Update(TEntity entity)
        {
            entities.AddOrUpdate(entity);
            dbContext.SaveChanges();

            return entity;
        }
    }
}
