using System.Collections.Generic;
using Entities.Entities;

namespace Core.DataAccess
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByName(string categoryName);
        List<Category> GetCategoriesWithProducts();
    }
}