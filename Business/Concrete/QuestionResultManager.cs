using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Core.Aspects.Autofac.Caching;
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
    public class QuestionResultManager : BusinessMessagesService, IQuestionResultService
    {
        IQuestionResultDal _questionResultDal;
        public QuestionResultManager(IQuestionResultDal questionResultDal)
        {
            _questionResultDal = questionResultDal;
        }

        [SecuredOperation("instructor, admin")]
        [CacheRemoveAspect("IQuestionResultService.Get")]
        public IResult Add(QuestionResult entity)
        {
            _questionResultDal.Add(entity);
            return new SuccessResult(_messages.QuestionResultCreated);
        }

        [CacheRemoveAspect("IQuestionResultService.Get")]
        public IResult AddWithDetails(QuestionResultDetailsDto questionResult)
        {
            var addedQuestionResult = questionResult.QuestionResult;
            Add(addedQuestionResult);
            return new SuccessResult(_messages.QuestionResultCreated);
        }

        [SecuredOperation("instructor, admin")]
        [CacheRemoveAspect("IQuestionResultService.Get")]
        public IResult Delete(QuestionResult entity)
        {
            _questionResultDal.Delete(entity);
            return new SuccessResult(_messages.QuestionResultDeleted);
        }

        public IDataResult<QuestionResult> Get(int id)
        {
            return new SuccessDataResult<QuestionResult>(_questionResultDal.Get(q => q.Id == id));
        }

        public IDataResult<List<QuestionResult>> GetAll()
        {
            return new SuccessDataResult<List<QuestionResult>>(_questionResultDal.GetAll());
        }

        [CacheAspect(30)]
        public IDataResult<List<QuestionResultDetailsDto>> GetAllDetails()
        {
            return new SuccessDataResult<List<QuestionResultDetailsDto>>(_questionResultDal.GetAllDetails());
        }

        [CacheAspect]
        public IDataResult<List<QuestionResultDetailsDto>> GetAllDetailsByTestResultId(int testResultId)
        {
            return new SuccessDataResult<List<QuestionResultDetailsDto>>(_questionResultDal.GetAllDetailsByTestResultId(testResultId));
        }

        [SecuredOperation("instructor, admin")]
        [CacheRemoveAspect("IQuestionResultService.Get")]
        public IResult Update(QuestionResult entity)
        {
            _questionResultDal.Update(entity);
            return new SuccessResult(_messages.QuestionResultUpdated);
        }
    }
}
