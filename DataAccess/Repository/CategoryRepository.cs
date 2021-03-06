﻿using System;
using Core.DataAccess;
using Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using Entities.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        private readonly AppDbContext _context;


        //DI
        public CategoryRepository(AppDbContext context):base(context)
        {
            _context = context;
        }





        public Category GetByName(string categoryName)
        {
            //return _context.Categories.Where(x => x.Name.ToLower() == categoryName.ToLower()).FirstOrDefault();
                return this.GetFirstOrDefault(x => x.Name.ToLower() == categoryName.ToLower());
        }




        public List<Category> GetCategoriesWithProducts()
        {
            return _context.Categories.Include(x => x.Products).ToList();
        }
    }
}