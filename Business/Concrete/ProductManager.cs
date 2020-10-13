using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Core.Log;
using Core.Results;
using Entities.Entities;
using Entities.Enums;
using FluentValidation;
using Repository.UnıtOfWork.Abstract;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly AbstractValidator<Product> _validator;

        public ProductManager(IUnitOfWork unitOfWork, ILogger logger, AbstractValidator<Product> validator)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _validator = validator;
        }




        public IResult Add(Product product)
        {
            var validateResult = _validator.Validate(product);

            if (!validateResult.IsValid)
            {
                return new ErrorResult(validateResult.Errors.FirstOrDefault().ErrorMessage);
            }

            if (_unitOfWork._productRepository.GetByName(product.Name) != null)
            {
                return new ErrorResult("Bu İsimde Bir Ürün Zaten Bulunmakta.");
            }


            _unitOfWork._productRepository.Add(product);
            _logger.Log(new EntityOperationLog()
            {
                TableName = "Products",
                LogType = (int)LogType.Added,
                OperationDate = DateTime.Now,
                LogData = _logger.SerializeObject(product)

            });

            _unitOfWork.Commit();
            //HATA OLUŞURSA EXCEPTİON HANDLER TARAFINDAN YAKALANIR, HİÇBİR İŞLEM GERÇEKLEŞMEZ.

            return new SuccessResult("Ürün Başarıyla Eklendi.");
        }




        public IResult AddRange(List<Product> products)
        {
            var validateResult = CheckProducts(products);

            if (!validateResult.Success)
            {
                return validateResult;
            }

            _unitOfWork._productRepository.AddRange(products);
            _logger.Log(new EntityOperationLog()
            {
                TableName = "Products",
                LogType = (int)LogType.Added,
                OperationDate = DateTime.Now,
                LogData = _logger.SerializeObject(products)
            });
            _unitOfWork.Commit();

            return new SuccessResult("Ürünler Başarıyla Eklendi");
        }




        public IResult Delete(int productID)
        {
            var findProduct = _unitOfWork._productRepository.GetById(productID);

            if (findProduct == null)
            {
                return new ErrorResult("Ürün Bulunamadı.");
            }

            _unitOfWork._productRepository.Delete(findProduct);
            _logger.Log(new EntityOperationLog()
            {
                TableName = "Products",
                LogType = (int)LogType.Deleted,
                OperationDate = DateTime.Now,
                LogData = _logger.SerializeObject(findProduct)
            });

            _unitOfWork.Commit();

            return new SuccessResult("Ürün Başarıyla Silindi.");
        }




        public IDataResult<List<Product>> GetAll()
        {
           return new SuccessDataResult<List<Product>>(_unitOfWork._productRepository.GetAll());
        }




        public IDataResult<Product> GetById(int productID)
        {
            var findProduct = _unitOfWork._productRepository.GetById(productID);


            if (findProduct == null)
            {
                return new ErrorDataResult<Product>("Ürün Bulunamadı.");
            }


            return new SuccessDataResult<Product>(findProduct);
        }





        public IResult Update(Product product)
        {

            //if (_unitOfWork._productRepository.GetById(product.ProductID) == null)
            //{
            //    return new ErrorResult(){Success = false, Message = "Güncellenecek Olan Ürün Veritabanında Bulunamadı"};
            //}


            if (_unitOfWork._productRepository.GetByName(product.Name) != null)
            {
                return new ErrorResult("Bu İsimde Bir Ürün Bulunmakta (Update)");
            }


            _unitOfWork._productRepository.Update(product);
            _logger.Log(new EntityOperationLog()
            {
                TableName = "Products",
                LogType = (int)LogType.Updated,
                OperationDate = DateTime.Now,
                LogData = _logger.SerializeObject(product)

            });
            _unitOfWork.Commit();

            return new SuccessResult("Ürün Başarıyla Güncellendi");

        }



        public IDataResult<Product> GetByName(string productName)
        {
            var product = _unitOfWork._productRepository.GetByName(productName);

            if (product == null)
            {
                return new ErrorDataResult<Product>("Ürün Bulunamadı");
            }

            return new SuccessDataResult<Product>(product);
        }




        public IDataResult<List<Product>> GetByCategory(int categoryID)
        {
            //CATEGORY VAR MI KONTROLÜ

            var products = _unitOfWork._productRepository.GetByCategory(categoryID);


            if (products.Count() == 0)
            {
                return new ErrorDataResult<List<Product>>("Bu Kategoride Ürün Bulunamadı.");
            }

            return new SuccessDataResult<List<Product>>(products);
        }





        private IResult CheckProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                var validateResult = _validator.Validate(product);


                if (!validateResult.IsValid)
                {
                    return new ErrorResult($"{product.Name} İsimli Ürün İçin : {validateResult.Errors.FirstOrDefault().ErrorMessage}");
                }


                if (_unitOfWork._productRepository.GetByName(product.Name) != null)
                {
                    return new ErrorResult($"{product.Name} İsimli Ürün Zaten Mevcut.");
                }
            }

            return new SuccessResult(){Success = true};
        }
    }
}
