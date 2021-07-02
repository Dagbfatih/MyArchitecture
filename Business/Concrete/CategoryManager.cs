using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        IQuestionCategoryService _categoryService;
        public CategoryManager(ICategoryDal categoryDal, IQuestionCategoryService categoryService)
        {
            _categoryDal = categoryDal;
            _categoryService = categoryService;
        }

        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IResult Delete(Category category)
        {
            DeleteRelations(category);
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        public IDataResult<Category> Get(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == id));
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        public IDataResult<List<Category>> GetAllByCategoryName(string categoryName)
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(c => c.CategoryName == categoryName));
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }

        private void DeleteRelations(Category category)
        {
            var deletedQuestionCategories = _categoryService.GetAll().Data.Where(qc => qc.CategoryId == category.CategoryId);
            if (deletedQuestionCategories == null)
            {
                return;
            }
            foreach (var questionCategory in deletedQuestionCategories)
            {
                _categoryService.Delete(questionCategory);
            }
        }

        
    }
}
