using Business.Abstract;
using Business.Constants;
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
    public class TestQuestionManager : ITestQuestionService
    {
        ITestQuestionDal _testQuestionDal;
        public TestQuestionManager(ITestQuestionDal testQuestionDal)
        {
            _testQuestionDal = testQuestionDal;
        }

        [CacheRemoveAspect("ITestQuestionService.Get")]
        public IResult Add(TestQuestion testQuestion)
        {
            _testQuestionDal.Add(testQuestion);
            return new SuccessResult(Messages.QuestionAddedToTest);
        }

        [CacheRemoveAspect("ITestQuestionService.Get")]
        public IResult Delete(TestQuestion testQuestion)
        {
            _testQuestionDal.Delete(testQuestion);
            return new SuccessResult(Messages.QuestionDeletedFromTest);
        }

        [CacheAspect(duration: 10)]
        public IDataResult<TestQuestion> Get(int id)
        {
            throw new NotImplementedException();
        }

        [CacheAspect(duration: 10)]
        [PerformanceAspect(5)]
        public IDataResult<List<TestQuestion>> GetAll()
        {
            return new SuccessDataResult<List<TestQuestion>>(_testQuestionDal.GetAll());
        }

        public IDataResult<List<TestQuestion>> GetTestQuestionsByQuestionId(int questionId)
        {
            return new SuccessDataResult<List<TestQuestion>>(_testQuestionDal.GetAll(tq => tq.QuestionId == questionId));
        }

        [TransactionScopeAspect()]
        public IResult TransactionalOperation(TestQuestion testQuestion)
        {
            _testQuestionDal.Add(testQuestion);
            _testQuestionDal.Update(testQuestion);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ITestQuestionService.Get")]
        public IResult Update(TestQuestion testQuestion)
        {
            _testQuestionDal.Update(testQuestion);
            return new SuccessResult(Messages.QuestionUpdatedForTest);
        }

        
    }
}
