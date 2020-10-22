using System;
using Core.DataAccess;
using Entities.Dto;
using Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Entities.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
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






        public List<GetProductWithCategoryDto> GetProductsWithCategory()
        {
            return _context.Products.Include(x => x.Category).Select(x => new GetProductWithCategoryDto()
            {
                ProductID = x.ProductID,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                CategoryName = x.Category.Name
            }).ToList();
        }
    }
}