using Core.DataAccess;
using Core.UnitOfWork;
using Entities.DbContext;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, IProductRepository products, ICategoryRepository categories)
        {
            _context = context;
            Products = products;
            Categories = categories;
        }


        public IProductRepository Products { get; set; }
        public ICategoryRepository Categories { get; set; }

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