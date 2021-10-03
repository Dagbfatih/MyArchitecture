using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Core.Aspects.Autofac.Transaction;
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
    public class TestResultManager : BusinessMessagesService, ITestResultService
    {
        private readonly ITestResultDal _testResultDal;
        private readonly IQuestionResultService _questionResultService;
        public TestResultManager(ITestResultDal testResultDal, IQuestionResultService questionResultService)
        {
            _testResultDal = testResultDal;
            _questionResultService = questionResultService;
        }


        public IResult Add(TestResult entity)
        {
            _testResultDal.Add(entity);
            return new SuccessResult(_messages.TestResultCreated);
        }

        [TransactionScopeAspect]
        public IResult AddWithDetails(TestResultDetailsDto testResult)
        {
            var addedTestResult = new TestResult
            {
                TestId = testResult.ResultDetails.TestId,
                UserId = testResult.ResultDetails.UserId
            };
            var testResultId = _testResultDal.Add(addedTestResult).Id;

            foreach (var questionResult in testResult.QuestionResults)
            {
                questionResult.QuestionResult.TestResultId = testResultId;
                _questionResultService.AddWithDetails(questionResult);
            }

            return new SuccessResult(_messages.TestResultCreated);
        }

        [SecuredOperation("admin")]
        public IResult Delete(TestResult entity)
        {
            _testResultDal.Delete(entity);
            return new SuccessResult(_messages.TestResultDeleted);
        }

        public IDataResult<TestResult> Get(int id)
        {
            return new SuccessDataResult<TestResult>(_testResultDal.Get(t => t.Id == id));
        }

        public IDataResult<List<TestResult>> GetAll()
        {
            return new SuccessDataResult<List<TestResult>>(_testResultDal.GetAll());
        }

        public IDataResult<List<TestResultDetailsDto>> GetAllDetails()
        {
            return new SuccessDataResult<List<TestResultDetailsDto>>(_testResultDal.GetAllDetails());
        }

        [SecuredOperation("user", true)]
        public IDataResult<List<TestResultDetailsDto>> GetAllDetailsByUser(int userId)
        {
            return new SuccessDataResult<List<TestResultDetailsDto>>(_testResultDal.GetAllDetailsByUser(userId));
        }

        public IDataResult<TestResultDetailsDto> GetDetailsById(int id)
        {
            return new SuccessDataResult<TestResultDetailsDto>(_testResultDal.GetDetailsById(id));
        }

        [SecuredOperation("admin")]
        public IResult Update(TestResult entity)
        {
            _testResultDal.Update(entity);
            return new SuccessResult(_messages.TestResultUpdated);
        }
    }
}
