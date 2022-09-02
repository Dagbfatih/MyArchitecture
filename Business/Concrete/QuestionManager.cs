using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Services;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
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
    public class QuestionManager : BusinessMessagesService, IQuestionService
    {
        IQuestionDal _questionDal;
        IOptionService _optionService;
        ITestQuestionService _testQuestionService;

        public QuestionManager(
            IQuestionDal questionDal,
            IOptionService optionService,
            ITestQuestionService testQuestionService)
        {
            _questionDal = questionDal;
            _optionService = optionService;
            _testQuestionService = testQuestionService;
        }

        [ValidationAspect(typeof(QuestionValidator))]
        [CacheRemoveAspect("IQuestionService.Get")]
        [SecuredOperation("instructor")]
        public IResult Add(Question question)
        {
            _questionDal.Add(question);
            return new SuccessResult(_messages.QuestionAdded);
        }

        [CacheRemoveAspect("IQuestionService.Get")]
        public IDataResult<Question> AddWithId(Question question)
        {
            var result = _questionDal.Add(question);
            return new SuccessDataResult<Question>(result, _messages.QuestionAdded);
        }

        [ValidationAspect(typeof(QuestionValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IQuestionService.Get")]
        public IResult AddWithDetails(QuestionDetailsDto question)
        {
            var addedQuestion = question.Question;
            question.Question.QuestionId = this.AddWithId(addedQuestion).Data.QuestionId;

            AddRelations(question);
            return new SuccessResult(_messages.QuestionAdded);
        }



        [TransactionScopeAspect]
        private void AddRelations(QuestionDetailsDto question)
        {
            foreach (var option in question.Options)
            {
                option.QuestionId = question.Question.QuestionId;
                _optionService.Add(option);
                Thread.Sleep(100);
            }
        }

        [ValidationAspect(typeof(QuestionValidator))]
        [TransactionScopeAspect]
        [SecuredOperation("instructor")]
        [CacheRemoveAspect("IQuestionService.Get")]
        public IResult UpdateWithDetails(QuestionDetailsDto question)
        {
            var updatedQuestion = question.Question;

            _questionDal.Update(updatedQuestion);

            UpdateRelations(question);
            return new SuccessResult(_messages.QuestionUpdated);
        }

        [TransactionScopeAspect]
        private void UpdateRelations(QuestionDetailsDto question)
        {
            var defaultOptions = _optionService.GetAllByQuestionId(question.Question.QuestionId).Data;

            foreach (var option in defaultOptions)
            {
                if (!question.Options.Any(o => o.Id == option.Id))
                {
                    _optionService.Delete(option);
                }
            }

            foreach (var option in question.Options)
            {
                option.QuestionId = question.Question.QuestionId;

                if (_optionService.Get(option.Id).Data == null)
                {
                    _optionService.Add(option);
                }
                else
                {
                    _optionService.Update(option);
                }

                Thread.Sleep(100);
            }
        }

        [CacheRemoveAspect("IQuestionService.Get")]
        [TransactionScopeAspect]
        [SecuredOperation("instructor")]
        public IResult Delete(Question question)
        {
            _questionDal.Delete(question);
            DeleteRelations(question);
            return new SuccessResult(_messages.QuestionDeleted);
        }

        private void DeleteRelations(Question question)
        {
            var deletedOptions = _optionService.GetAllByQuestionId(question.QuestionId).Data;
            var deletedTestQuestions = _testQuestionService.GetTestQuestionsByQuestionId(question.QuestionId).Data;

            foreach (var option in deletedOptions)
            {
                _optionService.Delete(option);
            }

            foreach (var testQuestion in deletedTestQuestions)
            {
                _testQuestionService.Delete(testQuestion);
            }
        }

        [CacheAspect(duration: 10)]
        [PerformanceAspect(5)]
        public IDataResult<List<Question>> GetAll()
        {
            return new SuccessDataResult<List<Question>>(_questionDal.GetAll());
        }

        [CacheAspect(duration: 10)]
        public IDataResult<List<Question>> GetAllByOptionName(string optionName)
        {

            return new SuccessDataResult<List<Question>>(_questionDal.GetAll()); // q => SplitHelper.SplitStringToString(q.Options, Seperators.Comma).Any(e => e == optionName))
        }

        [CacheAspect(duration: 10)]
        public IDataResult<List<Question>> GetAllByOptionNumber(int optionNumber)
        {
            return new SuccessDataResult<List<Question>>(_questionDal.GetAll()); // q => SplitHelper.SplitStringToString(q.Options, Seperators.Comma).Count == optionNumber)
        }

        [CacheAspect(duration: 10)]
        public IDataResult<List<Question>> GetAllByStarQuestion()
        {
            return new SuccessDataResult<List<Question>>(_questionDal.GetAll(q => q.StarQuestion));
        }

        [ValidationAspect(typeof(QuestionValidator))]
        [CacheRemoveAspect("IQuestionService.Get")]
        public IResult Update(Question question)
        {
            _questionDal.Update(question);
            return new SuccessResult(_messages.QuestionUpdated);
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalOperation(Question question)
        {
            this.Add(question);
            this.Update(question);
            return new SuccessResult(_messages.QuestionUpdated);
        }

        [CacheAspect(duration: 10)]
        public IDataResult<QuestionDetailsDto> GetQuestionDetailsById(int questionId)
        {
            return new SuccessDataResult<QuestionDetailsDto>(_questionDal.GetQuestionDetailsById(questionId));
        }

        public IDataResult<List<QuestionDetailsDto>> GetQuestionsDetails()
        {
            return new SuccessDataResult<List<QuestionDetailsDto>>(_questionDal.GetQuestionDetails());
        }

        public IDataResult<List<QuestionDetailsDto>> GetDetailsByQuestionText(string text)
        {
            return new SuccessDataResult<List<QuestionDetailsDto>>(_questionDal.GetDetailsByQuestionText(text));
        }

        public IDataResult<Question> Get(int id)
        {
            return new SuccessDataResult<Question>(_questionDal.Get(q => q.QuestionId == id));

        }

        //[CacheAspect(duration: 10)]
        [PerformanceAspect(10)]
        public IDataResult<List<QuestionDetailsDto>> GetAllDetailsByPublic()
        {
            return new SuccessDataResult<List<QuestionDetailsDto>>(_questionDal.GetAllDetailsByPublic());
        }

        [SecuredOperation("admin, user", true)]
        public IDataResult<List<QuestionDetailsDto>> GetDetailsByUser(int userId)
        {
            return new SuccessDataResult<List<QuestionDetailsDto>>(_questionDal.GetQuestionDetailsByUser(userId));
        }

        [CacheAspect(duration: 10)]
        public IDataResult<List<QuestionDetailsDto>> GetAllDetailsBySubjectsAndPublic(params int[] subjects)
        {
            return new SuccessDataResult<List<QuestionDetailsDto>>(_questionDal.GetAllDetailsBySubjects(subjects));
        }

        [TransactionScopeAspect]
        [SecuredOperation("user, instructor")]
        [CacheRemoveAspect("IQuestionService.Get")]
        public IResult DeleteWithDetails(QuestionDetailsDto question)
        {
            _questionDal.Delete(question.Question);
            DeleteRelations(question.Question);
            return new SuccessResult(_messages.QuestionDeleted);
        }
    }
}
