using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Entities;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        //DI
        public ProductRepository(AppDbContext context):base(context)
        {
            _context = context;
        }


        public List<Product> GetByCategory(int categoryID)
        {
            return _context.Products.Where(x => x.CategoryID == categoryID).ToList();
        }



        public Product GetByName(string productName)
        {
            return _context.Products.Where(x => x.Name.ToLower().Contains(productName.ToLower())).FirstOrDefault();
        }
    }
}
