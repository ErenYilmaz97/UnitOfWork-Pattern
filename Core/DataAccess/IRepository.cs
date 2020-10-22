using System.Collections.Generic;
using Core.Entities;

namespace Core.DataAccess
{
    public interface IRepository<TEntity> where TEntity: IEntity
    {
        List<TEntity> GetAll();
        TEntity GetById(int entityID);
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}