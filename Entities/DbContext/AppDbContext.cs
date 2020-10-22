using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entities.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        
    }
}