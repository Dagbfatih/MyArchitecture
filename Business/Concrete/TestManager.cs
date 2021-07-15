using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class TestManager : ITestService
    {
        ITestDal _testDal;
        ITestQuestionDal _testQuestionDal;
        public TestManager(ITestDal testDal, ITestQuestionDal testQuestionDal)
        {
            _testDal = testDal;
            _testQuestionDal = testQuestionDal;
        }

        [CacheRemoveAspect("ITestService.Get")]
        public IResult Add(Test test)
        {
            _testDal.Add(test);
            return new SuccessResult(Messages.TestCreated);
        }

        [ValidationAspect(typeof(TestValidator))]
        [TransactionScopeAspect]
        public IResult AddWithDetails(TestDetailsDto testDetailsDto)
        {
            var addedTest = new Test
            {
                TestName = testDetailsDto.TestName,
                TestNotes = testDetailsDto.TestNotes,
                TestTime = testDetailsDto.TestTime,
                Privacy = testDetailsDto.Privacy,
                MixedCategory = testDetailsDto.MixedCategory,
                UserId = testDetailsDto.UserId
            };
            testDetailsDto.TestId = _testDal.Add(addedTest).Id;

            AddRelations(testDetailsDto);
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        private void AddRelations(TestDetailsDto testDetailsDto)
        {
            foreach (var question in testDetailsDto.Questions)
            {
                _testQuestionDal.Add(new TestQuestion
                {
                    TestId = testDetailsDto.TestId,
                    QuestionId = question.QuestionId
                });
            }
        }

        [CacheRemoveAspect("ITestService.Get")]
        public IResult Delete(Test test)
        {
            DeleteRelations(test);
            _testDal.Delete(test);
            return new SuccessResult(Messages.TestDeleted);
        }

        [CacheAspect(duration: 10)]
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
        public IResult Update(Test test)
        {
            _testDal.Update(test);
            return new SuccessResult(Messages.TestDeleted);
        }

        public IDataResult<List<TestDetailsDto>> GetTestDetails()
        {
            return new SuccessDataResult<List<TestDetailsDto>>(_testDal.GetTestDetails());
        }

        public IDataResult<TestDetailsDto> GetTestDetailsById(int id)
        {
            return new SuccessDataResult<TestDetailsDto>(_testDal.GetTestDetailsById(id));
        }

        public IResult DeleteWithDetails(TestDetailsDto testDetailsDto)
        {
            var test = new Test
            {
                Id = testDetailsDto.TestId,
                MixedCategory = testDetailsDto.MixedCategory,
                Privacy = testDetailsDto.Privacy,
                TestName = testDetailsDto.TestName,
                TestNotes = testDetailsDto.TestNotes,
                TestTime = testDetailsDto.TestTime,
                UserId = testDetailsDto.UserId
            };
            _testDal.Delete(test);
            DeleteRelations(test);

            return new SuccessResult(Messages.TestDeleted);
        }

        private void DeleteRelations(Test test)
        {
            var deletedTestQuestions = _testQuestionDal.GetAll(tq => tq.TestId == test.Id);
            if (deletedTestQuestions!=null)
            {
                foreach (var testQuestion in deletedTestQuestions)
                {
                    _testQuestionDal.Delete(testQuestion);
                }
            }
        }

        public IDataResult<List<TestDetailsDto>> GetTestDetailsByUser(int userId)
        {
            return new SuccessDataResult<List<TestDetailsDto>>(_testDal.GetTestDetailsByUser(userId));
        }

        [TransactionScopeAspect]
        public IResult UpdateWithDetails(TestDetailsDto testDetailsDto)
        {
            var test = new Test
            {
                Id = testDetailsDto.TestId,
                MixedCategory = testDetailsDto.MixedCategory,
                Privacy = testDetailsDto.Privacy,
                TestName = testDetailsDto.TestName,
                TestNotes = testDetailsDto.TestNotes,
                TestTime = testDetailsDto.TestTime,
                UserId = testDetailsDto.UserId
            };
            _testDal.Update(test);
            UpdateRelations(testDetailsDto);
            return new SuccessResult(Messages.TestUpdated);
        }

        [TransactionScopeAspect]
        private void UpdateRelations(TestDetailsDto test)
        {
            var defaultQuestions = _testQuestionDal.GetAll(t=>t.TestId==test.TestId);

            foreach (var question in defaultQuestions)
            {
                if (!test.Questions.Any(q => q.QuestionId == question.QuestionId))
                {
                    _testQuestionDal.Delete(question);
                }
            }

            foreach (var question in test.Questions)
            {
                var updatedTestQuestion = new TestQuestion
                {
                    QuestionId = question.QuestionId,
                    TestId = test.TestId
                };

                var exists = _testQuestionDal.Get(t => t.QuestionId == updatedTestQuestion.QuestionId && t.TestId == updatedTestQuestion.TestId);
                if (exists == null)
                {
                    _testQuestionDal.Add(updatedTestQuestion);
                }
                else
                {
                    updatedTestQuestion.Id = exists.Id;
                    _testQuestionDal.Update(updatedTestQuestion);
                }

                Thread.Sleep(100);
            }
        }

        
    } 
}
