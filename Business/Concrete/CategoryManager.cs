using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
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
    public class CategoryManager : BusinessMessagesService, ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [ValidationAspect(typeof(CategoryValidator))]
        [SecuredOperation("admin")]
        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(_messages.CategoryAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(_messages.CategoryDeleted);
        }

        public IDataResult<Category> Get(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == id));
        }

        [PerformanceAspect(5)]
        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        public IDataResult<List<Category>> GetAllByCategoryName(string categoryName)
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(c => c.CategoryName == categoryName));
        }

        [SecuredOperation("admin")]
        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(_messages.CategoryUpdated);
        }
    }
}
