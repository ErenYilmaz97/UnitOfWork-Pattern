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
    public class CategoryManager : ICategoryService
    {

        //ENCAPSULATION İÇİN PRIVATE
        //SADECE BU SINIF İÇERİSİNDEKİ METHOTLARDA KULLANILABİLİR, DIŞARIDAN ERİŞİLEMEZ.

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly AbstractValidator<Category> _validator;


        //DI
        public CategoryManager(IUnitOfWork unitOfWork, ILogger logger, AbstractValidator<Category> validator)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _validator = validator;
        }



        public IResult Add(Category category)
        {
            var validatorResult = _validator.Validate(category);

            //VALIDATION
            if (!validatorResult.IsValid)
            {
                return new ErrorResult(validatorResult.Errors.First().ErrorMessage);
            }


            if (_unitOfWork._categoryRepository.GetByName(category.Name) != null)
            {
                return new ErrorResult("Bu İsimde Bir Kategori Bulunmakta.");
            } 

            _unitOfWork._categoryRepository.Add(category);
            _logger.Log(new EntityOperationLog()
            {
                TableName = "Categories",
                LogType = (int)LogType.Added,
                OperationDate = DateTime.Now,
                LogData =  _logger.SerializeObject(category)
            });

            _unitOfWork.Commit();

            //SAVECHANGESDE BİR HATA OLUŞURSA BURADA HATA VERİR. EXCEPTION HANDLER YAKALAR.

            return new SuccessResult("Kategori Başarıyla Eklendi.");


        }





        public IResult AddRange(List<Category> categories)
        {
            var validateResult = CheckCategories(categories);

            if (!validateResult.Success)
            {
                return validateResult;
            }

            _unitOfWork._categoryRepository.AddRange(categories);
            _logger.Log(new EntityOperationLog()
            {
                TableName = "Products",
                LogType = (int)LogType.Added,
                OperationDate = DateTime.Now,
                LogData = _logger.SerializeObject(categories)
            });

            _unitOfWork.Commit();

            return new SuccessResult("Kategoriler Başarıyla Eklendi");
        }





        public IResult Delete(int categoryID)
        {
            var category = _unitOfWork._categoryRepository.GetById(categoryID);

            if (category == null)
            {
                return new ErrorResult("Kategori Bulunamadı.");
            }

            _unitOfWork._categoryRepository.Delete(category);
            _logger.Log(new EntityOperationLog()
            {
                TableName = "Categories",
                LogType = (int)LogType.Deleted,
                OperationDate = DateTime.Now,
                LogData = _logger.SerializeObject(category)
            });

            _unitOfWork.Commit();
            //HATA VARSA EXCEPTION HANDLER YAKALAR

            return new SuccessResult("Kategori Başarıyla Silindi.");
        }





        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_unitOfWork._categoryRepository.GetAll());
        }





        public IDataResult<Category> GetById(int categoryID)
        {
            var category = _unitOfWork._categoryRepository.GetById(categoryID);

            if (category == null)
            {
                return new ErrorDataResult<Category>("Kategori Bulunamadı.");
            }

            return new SuccessDataResult<Category>(category);
        }





        public IResult Update(Category category)
        {
            if (_unitOfWork._categoryRepository.GetByName(category.Name) != null)
            {
                return new ErrorResult("Bu İsimde Bir Kategori Bulunmakta.");
            }


            _unitOfWork._categoryRepository.Update(category);
            _logger.Log(new EntityOperationLog()
            {
                TableName = "Categories",
                LogType = (int)LogType.Updated,
                OperationDate = DateTime.Now,
                LogData = _logger.SerializeObject(category)
            });

            _unitOfWork.Commit();

            return new SuccessResult("Kategori Başarıyla Güncellendi");
        }





        public IDataResult<Category> GetByName(string categoryName)
        {
            var category = _unitOfWork._categoryRepository.GetByName(categoryName);

            if (category == null)
            {
                return new ErrorDataResult<Category>("Kategori Bulunamadı.");
            }

            return new SuccessDataResult<Category>(category);
        }





        public IDataResult<List<Category>> GetCategoriesWithProducts()
        {
            return new SuccessDataResult<List<Category>>(_unitOfWork._categoryRepository.GetCategoriesWithProducts());
        }



        private IResult CheckCategories(List<Category> categories)
        {
            foreach (Category category in categories)
            {
                var validateResult = _validator.Validate(category);

                if (!validateResult.IsValid)
                {
                    return new ErrorResult("Kategoriler Doğrulanamadı.");
                }

                if (_unitOfWork._categoryRepository.GetByName(category.Name) != null)
                {
                    return new ErrorResult($"{category.Name} İsminde Bir Kategori Zaten Mevcut.");
                }
            }

            return new SuccessResult();
        }


    }
}
