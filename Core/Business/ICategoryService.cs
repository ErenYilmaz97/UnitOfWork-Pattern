using System.Collections.Generic;
using Core.Results;
using Entities.Entities;

namespace Core.Business
{
    public interface ICategoryService
    {
        IResult Add(Category category);
        IResult Delete(int categoryID);
        IDataResult<List<Category>> GetAll();
        IDataResult<Category> GetById(int categoryID);
        IResult Update(Category category);
        IDataResult<Category> GetByName(string categoryName);
        IResult AddRange(List<Category> categories);
        IDataResult<List<Category>> GetCategoriesWithProducts();
        IDataResult<Category> GetCategoryWithProducts(int categoryId);
    }
}