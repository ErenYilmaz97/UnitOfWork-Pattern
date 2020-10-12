using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Repository.Abstract;
using Repository.UnıtOfWork.Abstract;

namespace Repository.UnıtOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository _productRepository { get; set; }
        public ICategoryRepository _categoryRepository { get; set; }
        private readonly AppDbContext _context;


        public UnitOfWork(IProductRepository productRepository, ICategoryRepository categoryRepository, AppDbContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }



        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
