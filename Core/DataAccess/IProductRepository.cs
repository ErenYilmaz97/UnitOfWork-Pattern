using System.Collections.Generic;
using Entities.Dto;
using Entities.Entities;

namespace Core.DataAccess
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetByName(string productName);
        List<Product> GetByCategory(int categoryID);
        List<GetProductWithCategoryDto> GetProductsWithCategory();
    }
}