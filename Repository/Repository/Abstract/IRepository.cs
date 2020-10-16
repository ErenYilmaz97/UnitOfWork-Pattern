using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Abstract
{
    //GENERIC OLARAK VERDİĞİN NESNE BİR IENTTİY OLMAK ZORUNDA

    public interface IRepository<TEntity> where TEntity: IEntity
    {
        TEntity GetById(int id);
        List<TEntity> GetAll();
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
