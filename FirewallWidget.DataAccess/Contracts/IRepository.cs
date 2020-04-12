using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FirewallWidget.DataAccess.Contracts
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        TEntity Create(TEntity entity);

        TEntity Read(TKey primaryKey);

        TEntity Update(TEntity entity);

        TKey Delete(TKey primaryKey);

        IEnumerable<TEntity> Read(Expression<Func<TEntity, bool>> predicate);
    }
}
