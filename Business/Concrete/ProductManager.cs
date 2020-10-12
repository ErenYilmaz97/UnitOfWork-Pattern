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
                return new ErrorResult(){Success = false, Message = validateResult.Errors.FirstOrDefault().ErrorMessage};
            }

            if (_unitOfWork._productRepository.GetByName(product.Name) != null)
            {
                return new ErrorResult(){Success = false, Message = "Bu İsimde Bir Ürün Zaten Bulunmakta."};
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

            return new SuccessResult(){Success = true, Message = "Ürün Başarıyla Eklendi."};
        }




        public IResult AddRange(List<Product> products)
        {
            var validateResult = ValidateProducts(products);

            if (!validateResult.Success)
            {
                return new ErrorResult(){Success = false, Message = "Ürünler Doğrulanamadı."};
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

            return new SuccessResult(){Success = true, Message = "Ürünler Başarıyla Eklendi"};
        }




        public IResult Delete(int productID)
        {
            var findProduct = _unitOfWork._productRepository.GetById(productID);

            if (findProduct == null)
            {
                return new ErrorResult(){Success = false, Message = "Ürün Bulunamadı."};
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

            return new SuccessResult(){Success = true, Message = "Ürün Başarıyla Silindi."};
        }




        public IDataResult<List<Product>> GetAll()
        {
           return new SuccessDataResult<List<Product>>(){Success = true, Data = _unitOfWork._productRepository.GetAll()};
        }




        public IDataResult<Product> GetById(int productID)
        {
            var findProduct = _unitOfWork._productRepository.GetById(productID);


            if (findProduct == null)
            {
                return new ErrorDataResult<Product>(){Success = false, Message = "Ürün Bulunamadı."};
            }


            return new SuccessDataResult<Product>(){Success = true, Data = findProduct};
        }





        public IResult Update(Product product)
        {

            //if (_unitOfWork._productRepository.GetById(product.ProductID) == null)
            //{
            //    return new ErrorResult(){Success = false, Message = "Güncellenecek Olan Ürün Veritabanında Bulunamadı"};
            //}


            if (_unitOfWork._productRepository.GetByName(product.Name) != null)
            {
                return new ErrorResult(){Success = false, Message = "Bu İsimde Bir Ürün Bulunmakta (Update)"};
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

            return new SuccessResult(){Success = true, Message = "Ürün Başarıyla Güncellendi"};

        }




        private IResult ValidateProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                var validateResult = _validator.Validate(product);

                if (!validateResult.IsValid)
                {
                    return new ErrorResult(){Success = false};
                }
            }

            return new SuccessResult(){Success = true};
        }
    }
}
