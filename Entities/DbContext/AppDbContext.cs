using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EntityOperationLog> EntityOperationLogs { get; set; }

    }
}
