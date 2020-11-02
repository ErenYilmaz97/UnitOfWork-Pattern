using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;
using Microsoft.SqlServer.Management.Assessment.Expressions;
using Expression = Microsoft.SqlServer.Management.Assessment.Expressions.Expression;

namespace Core.DataAccess
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        List<TEntity> GetAll();
        TEntity GetById(int entityID);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}