using System;
using Core.DataAccess;

namespace Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
         public IProductRepository Products { get; set; }
         public ICategoryRepository Categories { get; set; }
         //DİĞER TÜM REPOLAR

         int Commit();
    }
}