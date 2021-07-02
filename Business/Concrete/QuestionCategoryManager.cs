using Business.Abstract;
using Business.Constants;
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
    public class QuestionCategoryManager : IQuestionCategoryService
    {
        IQuestionCategoryDal _questionCategoryDal;

        public QuestionCategoryManager(IQuestionCategoryDal questionCategory)
        {
            _questionCategoryDal = questionCategory;
        }

        public IResult Add(QuestionCategory questionCategory)
        {
            var result = BusinessRules.Run(CheckIfCategoryExistsOnQuestion(questionCategory));
            if (result != null)
            {
                return result;
            }
            _questionCategoryDal.Add(questionCategory);
            return new SuccessResult(Messages.QuestionCategoryAdded);
        }

        private IResult CheckIfCategoryExistsOnQuestion(QuestionCategory questionCategory)
        {
            var result = this.GetCategoriesByQuestionId(questionCategory.QuestionId);
            foreach (var category in result.Data)
            {
                if (category.CategoryId == questionCategory.CategoryId)
                {
                    return new ErrorResult(Messages.CategoryExists);
                }
            }
            return new SuccessResult();
        }

        public IResult Delete(QuestionCategory questionCategory)
        {
            _questionCategoryDal.Delete(questionCategory);
            return new SuccessResult(Messages.QuestionCategoryDeleted);
        }

        public IDataResult<QuestionCategory> Get(int id)
        {
            return new SuccessDataResult<QuestionCategory>(_questionCategoryDal.Get(qc => qc.Id == id));
        }

        public IDataResult<List<QuestionCategory>> GetAll()
        {
            return new SuccessDataResult<List<QuestionCategory>>(_questionCategoryDal.GetAll());
        }

        public IDataResult<List<QuestionCategory>> GetCategoriesByQuestionId(int questionId)
        {
            return new SuccessDataResult<List<QuestionCategory>>(_questionCategoryDal.GetAll(qc => qc.QuestionId == questionId));
        }

        public IResult Update(QuestionCategory questionCategory)
        {
            _questionCategoryDal.Update(questionCategory);
            return new SuccessResult(Messages.QuestionCategoryUpdated);
        }
    }
}
