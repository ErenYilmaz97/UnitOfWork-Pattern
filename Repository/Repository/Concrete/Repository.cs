using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class,IEntity,new()
    {
        private readonly AppDbContext _context;

        //DI
        public Repository(AppDbContext context)
        {
            _context = context;
        }




        public TEntity GetById(int entityID)
        {
            return _context.Set<TEntity>().Find(entityID);
        }


        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
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


        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
