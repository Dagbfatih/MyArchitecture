using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class QuestionResultManager : IQuestionResultService
    {
        IQuestionResultDal _questionResultDal;
        public QuestionResultManager(IQuestionResultDal questionResultDal)
        {
            _questionResultDal = questionResultDal;
        }
        public IResult Add(QuestionResult entity)
        {
            _questionResultDal.Add(entity);
            return new SuccessResult(Messages.QuestionResultCreated);
        }

        public IResult AddWithDetails(QuestionResultDetailsDto questionResult)
        {
            var addedQuestionResult = new QuestionResult
            {
                Accuracy = questionResult.Accuracy,
                CorrectOptionId = questionResult.CorrectOptionId,
                QuestionId = questionResult.QuestionId,
                SelectedOptionId = questionResult.SelectedOptionId,
                TestResultId = questionResult.TestResultId
            };

            _questionResultDal.Add(addedQuestionResult);
            return new SuccessResult(Messages.QuestionResultCreated);
        }

        public IResult Delete(QuestionResult entity)
        {
            _questionResultDal.Delete(entity);
            return new SuccessResult(Messages.QuestionResultDeleted);
        }

        public IDataResult<QuestionResult> Get(int id)
        {
            return new SuccessDataResult<QuestionResult>(_questionResultDal.Get(q => q.Id == id));
        }

        public IDataResult<List<QuestionResult>> GetAll()
        {
            return new SuccessDataResult<List<QuestionResult>>(_questionResultDal.GetAll());
        }

        public IDataResult<List<QuestionResultDetailsDto>> GetAllDetails()
        {
            return new SuccessDataResult<List<QuestionResultDetailsDto>>(_questionResultDal.GetAllDetails());
        }

        public IDataResult<List<QuestionResultDetailsDto>> GetAllDetailsByTestResultId(int testResultId)
        {
            return new SuccessDataResult<List<QuestionResultDetailsDto>>(_questionResultDal.GetAllDetailsByTestResultId(testResultId));
        }

        public IResult Update(QuestionResult entity)
        {
            _questionResultDal.Update(entity);
            return new SuccessResult(Messages.QuestionResultUpdated);
        }
    }
}
