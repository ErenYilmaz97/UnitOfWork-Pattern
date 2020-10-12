using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;

namespace Repository.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        //IREPOSITORY METHOTLARINI İÇERMEK ZORUNDA

        //EK OLARAK BU NESNEYE ÖZEL İŞLEMLER

        Product GetByName(string productName);
        List<Product> GetByCategory(int categoryID);
    }
}
