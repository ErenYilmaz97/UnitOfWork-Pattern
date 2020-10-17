using Core.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Dto;
using Entities.Entities;

namespace Business.Abstract
{
    public interface IProductService
    {
        IResult Add(Product product);
        IResult AddRange(List<Product> products);
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int productID);
        IResult Delete(int productID);
        IResult Update(Product product);
        IDataResult<Product> GetByName(string productName);
        IDataResult<List<Product>> GetByCategory(int categoryID);
        IDataResult<List<GetProductsWithCategoryDto>> GetProductsWithCategory();
        IDataResult<GetProductsWithCategoryDto> GetProductWithCategory(int ProductID);
    }
}
