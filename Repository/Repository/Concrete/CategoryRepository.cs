using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        private readonly AppDbContext _context;

        //DI
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }



        public Category GetByName(string categoryName)
        {
            return _context.Categories.Where(x => x.Name.ToLower() == categoryName.ToLower()).FirstOrDefault();
        }



        public List<Category> GetCategoriesWithProducts()
        {
            return _context.Categories.Include(x => x.Products).ToList();
        }
    }
}
