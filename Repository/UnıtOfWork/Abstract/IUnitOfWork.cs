using System;
using System.Collections.Generic;
using System.Text;
using Repository.Abstract;

namespace Repository.UnıtOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        //TÜM REPOSİTORYLER BURADA TOPLANACAK

        public IProductRepository _productRepository { get; set; }
        public ICategoryRepository _categoryRepository { get; set; }


        //SAVECHANGES
        int Commit();

    }
}
