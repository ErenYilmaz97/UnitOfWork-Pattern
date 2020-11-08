using System;
using System.Collections.Generic;
using System.Linq;
using Business.ValidationRules.FluentValidation.Validators;
using Core;
using Core.Business;
using Core.Enums;
using Core.Results;
using Core.UnitOfWork;
using Entities.Dto;
using Entities.Entities;
using FluentValidation;

namespace Business
{
    public class ProductManager : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly AbstractValidator<Product> _validator;
        private readonly ILogManager _logManager;

        public ProductManager(IUnitOfWork unitOfWork, AbstractValidator<Product> validator, ILogManager logManager)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logManager = logManager;
        }





        public IResult Add(Product product)
        {
            if (GetByName(product.Name).Success)
            {
                return new ErrorResult("Bu İsimde Bir Ürün Bulunmakta.");
            }

            _unitOfWork.Products.Add(product);
            _unitOfWork.Commit();

            //HATA OLURSA EXCEPTİON HANDLER YAKALAR


            //İŞLEM BAŞARILIYSA LOGLA
            _logManager.GetLogger().Information("{@product}",product, LogType.Added);
            return new SuccessResult("Ürün Başarıyla Eklendi.");
        }






        public IResult AddRange(List<Product> products)
        {
            var result = CheckProducts(products);

            if (!result.Success)
            {
                //HATA MEVCUT
                return result;
            }


            _unitOfWork.Products.AddRange(products);
            _unitOfWork.Commit();


            //İŞLEM BAŞARILIYSA LOGLA
            _logManager.GetLogger().Information("{@products}",products, LogType.Added);
            return new SuccessResult("Ürünler Başarıyla Eklendi.");
        }





        public IResult Delete(int productID)
        {
            var getProductResult = GetById(productID);

            if (!getProductResult.Success)
            {
                return new ErrorResult("Ürün Bulunamadı");
            }

            _unitOfWork.Products.Delete(getProductResult.Data);
            _unitOfWork.Commit();


            //İŞLEM BAŞARILIYSA LOGLA
            _logManager.GetLogger().Information("{@product}",getProductResult.Data, LogType.Deleted);
            return new SuccessResult("Ürün Başarıyla Silindi.");
        }






        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_unitOfWork.Products.GetAll());
        }





        public IDataResult<List<Product>> GetByCategory(int categoryID)
        {
            if (_unitOfWork.Categories.GetById(categoryID) == null)
            {
                return new ErrorDataResult<List<Product>>("Seçilen Kategori Bulunamadı");
            }


            var productsByCategory = _unitOfWork.Products.GetByCategory(categoryID);


            if (productsByCategory.Count == 0)
            {
                return new ErrorDataResult<List<Product>>("Bu Kategoride Bir Ürün Bulunamadı.");
            }


            return new SuccessDataResult<List<Product>>(productsByCategory);
        }





        public IDataResult<Product> GetById(int productID)
        {
            var product = _unitOfWork.Products.GetById(productID);

            if (product == null)
            {
                return new ErrorDataResult<Product>("Ürün Bulunamadı.");
            }


            return new SuccessDataResult<Product>(product);
        }






        public IDataResult<Product> GetByName(string productName)
        {
            var product = _unitOfWork.Products.GetByName(productName);

            if (product == null)
            {
                return new ErrorDataResult<Product>("Ürün Bulunamadı.");
            }

            return new SuccessDataResult<Product>(product);
        }







        public IDataResult<List<GetProductWithCategoryDto>> GetProductsWithCategory()
        {
            var products = _unitOfWork.Products.GetProductsWithCategory();


            if (products.Count == 0)
            {
                return new ErrorDataResult<List<GetProductWithCategoryDto>>("Ürün Bulunamadı.");
            }

            return new SuccessDataResult<List<GetProductWithCategoryDto>>(products);


        }







        public IDataResult<GetProductWithCategoryDto> GetProductWithCategory(int ProductID)
        {
            var product = _unitOfWork.Products.GetProductsWithCategory().Where(x => x.ProductID == ProductID).FirstOrDefault();
           

            if (product == null)
            {
                return new ErrorDataResult<GetProductWithCategoryDto>("Ürün Bulunamadı.");
            }

            return new SuccessDataResult<GetProductWithCategoryDto>(product);
        }






        public IResult Update(Product product)
        {

            var findProduct = _unitOfWork.Products.GetById(product.ProductID);

            if (findProduct == null)
            {
                return new ErrorResult("Güncellenecek Ürün Bulunamadı.");
            }


            if (_unitOfWork.Categories.GetById(product.CategoryID) == null)
            {
                return new ErrorResult("Gelen ID'li Kategori Bulunamadı.");
            }


            if (_unitOfWork.Products.GetByName(product.Name) != null)
            {
                return new ErrorResult("Bu İsimde Bir Ürün Bulunmakta. (Update)");
            }

            findProduct.Name = product.Name;
            findProduct.Price = product.Price;
            findProduct.Stock = product.Stock;
            findProduct.CategoryID = product.CategoryID;

            _unitOfWork.Products.Update(findProduct);
            _unitOfWork.Commit();


            //İŞLEM BAŞARILIYSA LOGLA
            _logManager.GetLogger().Information("{@product}",product, LogType.Updated);
            return new SuccessResult("Ürün Başarıyla Güncellendi.");
        }






        private IResult CheckProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                var validateResult = _validator.Validate(product);

                //ISVALID
                if (!validateResult.IsValid)
                {
                    return new ErrorResult($"{product.Name} İsimli Ürün İçin : {validateResult.Errors.First().ErrorMessage}");
                }


                //DBDE MEVCUT MU
                if (GetByName(product.Name).Success)
                {
                    return new ErrorResult($"{product.Name} İsimli Ürün İçin : Bu İsimde Bir Ürün Bulunmakta.");
                }
            }

            return new SuccessResult();
        }

    }
}