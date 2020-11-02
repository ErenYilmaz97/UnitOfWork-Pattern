using System;
using Core.DataAccess;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Entities.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

        private readonly AppDbContext _context;


        //DI
        public Repository(AppDbContext context)
        {
            _context = context;
        }



        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }


        public void AddRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }


        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public List<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }


        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }


        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }


        public TEntity GetById(int entityID)
        {
            return _context.Set<TEntity>().Find(entityID);
        }

        

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}