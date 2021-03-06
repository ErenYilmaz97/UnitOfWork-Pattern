﻿using System.Collections.Generic;
using Core.Results;
using Entities.Dto;
using Entities.Entities;

namespace Core.Business
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
        IDataResult<List<GetProductWithCategoryDto>> GetProductsWithCategory();
        IDataResult<GetProductWithCategoryDto> GetProductWithCategory(int ProductID);
    }
}