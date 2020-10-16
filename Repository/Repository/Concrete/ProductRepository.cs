using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Dto;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                return _context.Products.Where(x => x.Name.ToLower() == productName.ToLower()).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }





        public List<GetProductsWithCategoryDto> GetProductsWithCategory()
        {
            return _context.Products.Include(x => x.Category).Select(x => new GetProductsWithCategoryDto()
            {
                ProductID = x.ProductID,
                ProductName = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                CategoryID = x.CategoryID,
                CategoryName = x.Category.Name
            }).ToList();
        }

    }
}
