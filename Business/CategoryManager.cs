using Core.Business;
using Core.Results;
using Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Enums;
using Core.UnitOfWork;
using FluentValidation;

namespace Business
{
    public class CategoryManager : ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly AbstractValidator<Category> _validator;
        private readonly ILogManager _logManager;


        //DI
        public CategoryManager(IUnitOfWork unitOfWork, AbstractValidator<Category> validator, ILogManager logManager)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logManager = logManager;
        }




        public IResult Add(Category category)
        {
            if (GetByName(category.Name).Success)
            {
                return new ErrorResult("Bu İsimde Bir Kategori Bulunmakta.");
            }

            _unitOfWork.Categories.Add(category);
            _unitOfWork.Commit();

            //HATA OLUŞURSA EXCEPTION HANDLER YAKALAR


            //İŞLEM BAŞARILIYSA LOGLA
            _logManager.GetLogger().Information("{@category}",category, LogType.Added);
            return new SuccessResult("Kategori Başarıyla Eklendi");
        }





        public IResult AddRange(List<Category> categories)
        {

            var result = CheckCategories(categories);

            if (!result.Success)
            {
                //HATA MEVCUT
                return result;
            }


            _unitOfWork.Categories.AddRange(categories);
            _unitOfWork.Commit();

            //İŞLEM BAŞARILIYSA LOGLA
            _logManager.GetLogger().Information("{@categories}",categories, LogType.Added);
            return new SuccessResult("Kategoriler Başarıyla Eklendi");
        }






        public IResult Delete(int categoryID)
        {
            var findCategoryResult = GetById(categoryID);

            if (!findCategoryResult.Success)
            {
                return new ErrorResult("Kategori Bulunamadı.");
            }


            _unitOfWork.Categories.Delete(findCategoryResult.Data);
            _unitOfWork.Commit();

            //İŞLEM BAŞARILIYSA LOGLA
            _logManager.GetLogger().Information("{@category}",findCategoryResult.Data, LogType.Deleted);
            return new SuccessResult("Kategori Başarıyla Silindi.");
        }





        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_unitOfWork.Categories.GetAll());
        }





        public IDataResult<Category> GetById(int categoryID)
        {
            var category = _unitOfWork.Categories.GetById(categoryID);

            if (category == null)
            {
                return new ErrorDataResult<Category>("Kategori Bulunamadı");
            }

            return new SuccessDataResult<Category>(category);
        }






        public IDataResult<Category> GetByName(string categoryName)
        {
            var category = _unitOfWork.Categories.GetByName(categoryName);

            if (category == null)
            {
                return new ErrorDataResult<Category>("Kategori Bulunamadı.");
            }

            return new SuccessDataResult<Category>(category);
        }






        public IDataResult<List<Category>> GetCategoriesWithProducts()
        {
            var categoriesWithProducts = _unitOfWork.Categories.GetCategoriesWithProducts();

            if (categoriesWithProducts.Count == 0)
            {
                return new ErrorDataResult<List<Category>>("Kategori Bulunamadı.");
            }

            return new SuccessDataResult<List<Category>>(categoriesWithProducts);
        }






        public IDataResult<Category> GetCategoryWithProducts(int categoryId)
        {
            var categoryWithProducts = _unitOfWork.Categories.GetCategoriesWithProducts().Where(x=>x.CategoryID == categoryId).FirstOrDefault();

            if (categoryWithProducts == null)
            {
                return new ErrorDataResult<Category>("Kategori Bulunamadı.");
            }

            return new SuccessDataResult<Category>(categoryWithProducts);
        }





        public IResult Update(Category category)
        {
            var findCategory = _unitOfWork.Categories.GetById(category.CategoryID);

            if (findCategory == null)
            {
                return new ErrorResult("Güncellenecek Kategori Bulunamadı.");
            }


            if (_unitOfWork.Categories.GetByName(category.Name) != null)
            {
                return new ErrorResult("Bu İsimde Bir Kategori Zaten Mevcut. (Update)");
            }


            findCategory.Name = category.Name;


            _unitOfWork.Categories.Update(findCategory);
            _unitOfWork.Commit();

            //İŞLEM BAŞARILIYSA LOGLA
            _logManager.GetLogger().Information("{@category}",category, LogType.Updated);
            return new SuccessResult("Kategori Başarıyla Güncellendi.");
        }




        private IResult CheckCategories(List<Category> categories)
        {
            foreach (Category category in categories)
            {
                var validatorResult = _validator.Validate(category);

                //IS VALID
                if (!validatorResult.IsValid)
                {
                    return new ErrorResult($"{category.Name} İsimli Kategori İçin :  {validatorResult.Errors.First().ErrorMessage}");
                }

                //DBDE MEVCUT MU
                if (GetByName(category.Name).Success)
                {
                    return new ErrorResult($"{category.Name} İsimli Kategori İçin : Bu İsimde Bir Kategori Mevcut.");
                }
            }

            return new SuccessResult();
        }
    }
}