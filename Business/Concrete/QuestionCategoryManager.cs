using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Core.Aspects.Autofac.Performance;
using Core.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class QuestionCategoryManager :BusinessMessagesService, IQuestionCategoryService
    {
        IQuestionCategoryDal _questionCategoryDal;

        public QuestionCategoryManager(IQuestionCategoryDal questionCategory)
        {
            _questionCategoryDal = questionCategory;
        }

        [SecuredOperation("instructor")]
        public IResult Add(QuestionCategory questionCategory)
        {
            var result = BusinessRules.Run(CheckIfCategoryExistsOnQuestion(questionCategory));
            if (result != null)
            {
                return result;
            }
            _questionCategoryDal.Add(questionCategory);
            return new SuccessResult(_messages.QuestionCategoryAdded);
        }

        private IResult CheckIfCategoryExistsOnQuestion(QuestionCategory questionCategory)
        {
            var result = this.GetCategoriesByQuestionId(questionCategory.QuestionId);
            foreach (var category in result.Data)
            {
                if (category.CategoryId == questionCategory.CategoryId)
                {
                    return new ErrorResult(_messages.CategoryExists);
                }
            }
            return new SuccessResult();
        }

        [SecuredOperation("instructor")]
        public IResult Delete(QuestionCategory questionCategory)
        {
            _questionCategoryDal.Delete(questionCategory);
            return new SuccessResult(_messages.QuestionCategoryDeleted);
        }

        public IDataResult<QuestionCategory> Get(int id)
        {
            return new SuccessDataResult<QuestionCategory>(_questionCategoryDal.Get(qc => qc.Id == id));
        }

        [PerformanceAspect(5)]
        public IDataResult<List<QuestionCategory>> GetAll()
        {
            return new SuccessDataResult<List<QuestionCategory>>(_questionCategoryDal.GetAll());
        }

        [PerformanceAspect(5)]
        public IDataResult<List<QuestionCategory>> GetCategoriesByQuestionId(int questionId)
        {
            return new SuccessDataResult<List<QuestionCategory>>(_questionCategoryDal.GetAll(qc => qc.QuestionId == questionId));
        }

        [SecuredOperation("instructor")]
        public IResult Update(QuestionCategory questionCategory)
        {
            _questionCategoryDal.Update(questionCategory);
            return new SuccessResult(_messages.QuestionCategoryUpdated);
        }
    }
}
