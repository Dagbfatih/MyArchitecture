using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class TestQuestionManager : BusinessMessagesService, ITestQuestionService
    {
        ITestQuestionDal _testQuestionDal;
        public TestQuestionManager(ITestQuestionDal testQuestionDal)
        {
            _testQuestionDal = testQuestionDal;
        }

        [CacheRemoveAspect("ITestQuestionService.Get")]
        [SecuredOperation("admin, instructor")]
        public IResult Add(TestQuestion testQuestion)
        {
            _testQuestionDal.Add(testQuestion);
            return new SuccessResult(_messages.QuestionAddedToTest);
        }

        [CacheRemoveAspect("ITestQuestionService.Get")]
        [SecuredOperation("admin, instructor")]
        public IResult Delete(TestQuestion testQuestion)
        {
            _testQuestionDal.Delete(testQuestion);
            return new SuccessResult(_messages.QuestionDeletedFromTest);
        }

        [CacheAspect(duration: 10)]
        public IDataResult<TestQuestion> Get(int id)
        {
            return new SuccessDataResult<TestQuestion>(_testQuestionDal.Get(t => t.Id == id));
        }

        [CacheAspect(duration: 10)]
        [PerformanceAspect(5)]
        public IDataResult<List<TestQuestion>> GetAll()
        {
            return new SuccessDataResult<List<TestQuestion>>(_testQuestionDal.GetAll());
        }

        public IDataResult<List<TestQuestion>> GetAllByTest(int testId)
        {
            return new SuccessDataResult<List<TestQuestion>>(_testQuestionDal.GetAll(t => t.TestId == testId));
        }

        public IDataResult<TestQuestion> GetAllByTestAndQuestion(int testId, int questionId)
        {
            return new SuccessDataResult<TestQuestion>(_testQuestionDal
                .Get(t => t.TestId == testId && t.QuestionId == questionId));
        }

        public IDataResult<List<TestQuestion>> GetTestQuestionsByQuestionId(int questionId)
        {
            return new SuccessDataResult<List<TestQuestion>>(_testQuestionDal.GetAll(tq => tq.QuestionId == questionId));
        }

        [CacheRemoveAspect("ITestQuestionService.Get")]
        [SecuredOperation("admin, instructor")]
        public IResult Update(TestQuestion testQuestion)
        {
            _testQuestionDal.Update(testQuestion);
            return new SuccessResult(_messages.QuestionUpdatedForTest);
        }


    }
}
