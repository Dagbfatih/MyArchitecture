using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Services;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Business.Concrete
{
    public class TestManager : BusinessMessagesService, ITestService
    {
        ITestDal _testDal;
        ITestQuestionService _testQuestionService;
        public TestManager(ITestDal testDal, ITestQuestionService testQuestionService)
        {
            _testDal = testDal;
            _testQuestionService = testQuestionService;
        }

        [CacheRemoveAspect("ITestService.Get")]
        [SecuredOperation("admin, instructor")]
        [ValidationAspect(typeof(TestValidator))]
        public IResult Add(Test test)
        {
            _testDal.Add(test);
            return new SuccessResult(_messages.TestCreated);
        }

        [CacheRemoveAspect("ITestService.Get")]
        [SecuredOperation("admin, instructor")]
        public IDataResult<int> AddWithId(Test test)
        {
            var result = _testDal.Add(test);
            return new SuccessDataResult<int>(result.Id, _messages.TestCreated);
        }

        [ValidationAspect(typeof(TestDetailsDtoValidator))]
        [TransactionScopeAspect]
        [SecuredOperation("admin, instructor")]
        [CacheRemoveAspect("ITestService.Get")]
        public IResult AddWithDetails(TestDetailsDto testDetailsDto)
        {
            var addedTest = testDetailsDto.Test;

            testDetailsDto.Test.Id = this.AddWithId(addedTest).Data;

            AddRelations(testDetailsDto);
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        [CacheRemoveAspect("ITestService.Get")]
        private void AddRelations(TestDetailsDto testDetailsDto)
        {
            foreach (var question in testDetailsDto.Questions)
            {
                _testQuestionService.Add(new TestQuestion
                {
                    TestId = testDetailsDto.Test.Id,
                    QuestionId = question.Question.QuestionId
                });
            }
        }

        [CacheRemoveAspect("ITestService.Get")]
        [SecuredOperation("admin, instructor")]
        public IResult Delete(Test test)
        {
            DeleteRelations(test);
            _testDal.Delete(test);
            return new SuccessResult(_messages.TestDeleted);
        }

        [CacheAspect(duration: 10)]
        [SecuredOperation("admin")]
        public IDataResult<List<Test>> GetAll()
        {
            return new SuccessDataResult<List<Test>>(_testDal.GetAll());
        }

        [CacheAspect(duration: 10)]
        public IDataResult<Test> Get(int id)
        {
            return new SuccessDataResult<Test>(_testDal.Get());
        }

        [CacheRemoveAspect("ITestService.Get")]
        [SecuredOperation("admin, instructor")]
        [ValidationAspect(typeof(TestValidator))]
        public IResult Update(Test test)
        {
            _testDal.Update(test);
            return new SuccessResult(_messages.TestDeleted);
        }

        [SecuredOperation("admin")]
        public IDataResult<List<TestDetailsDto>> GetTestDetails()
        {
            return new SuccessDataResult<List<TestDetailsDto>>(_testDal.GetTestDetails());
        }

        public IDataResult<TestDetailsDto> GetTestDetailsById(int id)
        {
            return new SuccessDataResult<TestDetailsDto>(_testDal.GetTestDetailsById(id));
        }

        [SecuredOperation("admin, instructor")]
        [CacheRemoveAspect("ITestService.Get")]
        public IResult DeleteWithDetails(TestDetailsDto testDetailsDto)
        {
            var test = testDetailsDto.Test;
            _testDal.Delete(test);
            DeleteRelations(test);

            return new SuccessResult(_messages.TestDeleted);
        }

        private void DeleteRelations(Test test)
        {
            var deletedTestQuestions = _testQuestionService.GetAllByTest(test.Id).Data;
            if (deletedTestQuestions != null)
            {
                foreach (var testQuestion in deletedTestQuestions)
                {
                    _testQuestionService.Delete(testQuestion);
                }
            }
        }

        [SecuredOperation("admin, instructor", true)]
        public IDataResult<List<TestDetailsDto>> GetTestDetailsByUser(int userId)
        {
            return new SuccessDataResult<List<TestDetailsDto>>(_testDal.GetTestDetailsByUser(userId));
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin, instructor")]
        [CacheRemoveAspect("ITestService.Get")]
        [ValidationAspect(typeof(TestDetailsDtoValidator))]
        public IResult UpdateWithDetails(TestDetailsDto testDetailsDto)
        {
            var test = testDetailsDto.Test;
            _testDal.Update(test);
            UpdateRelations(testDetailsDto);
            return new SuccessResult(_messages.TestUpdated);
        }

        [TransactionScopeAspect]
        private void UpdateRelations(TestDetailsDto test)
        {
            var defaultQuestions = _testQuestionService.GetAllByTest(test.Test.Id).Data;

            foreach (var question in defaultQuestions)
            {
                if (!test.Questions.Any(q => q.Question.QuestionId == question.QuestionId))
                {
                    _testQuestionService.Delete(question);
                }
            }

            foreach (var question in test.Questions)
            {
                var updatedTestQuestion = new TestQuestion
                {
                    QuestionId = question.Question.QuestionId,
                    TestId = test.Test.Id
                };

                var exists = _testQuestionService.
                    GetAllByTestAndQuestion(updatedTestQuestion.TestId, updatedTestQuestion.QuestionId).Data;
                if (exists == null)
                {
                    _testQuestionService.Add(updatedTestQuestion);
                }
                else
                {
                    updatedTestQuestion.Id = exists.Id;
                    _testQuestionService.Update(updatedTestQuestion);
                }

                Thread.Sleep(100);
            }
        }

        [CacheAspect(10)]
        public IDataResult<List<TestDetailsDto>> GetTestDetailsByPublic()
        {
            return new SuccessDataResult<List<TestDetailsDto>>(_testDal
                .GetTestDetails()
                .Where(t => t.Test.Privacy).ToList());
        }
    }
}
