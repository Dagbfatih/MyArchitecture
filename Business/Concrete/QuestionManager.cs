using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.CrossCuttingConcerns.Validation;
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
    public class QuestionManager : BusinessMessagesService, IQuestionService
    {
        IQuestionDal _questionDal;
        IOptionService _optionService;
        ITestQuestionService _testQuestionService;
        IQuestionCategoryService _questionCategoryService;

        public QuestionManager(
            IQuestionDal questionDal,
            IOptionService optionService,
            IQuestionCategoryService questionCategoryService,
            ITestQuestionService testQuestionService)
        {
            _questionDal = questionDal;
            _optionService = optionService;
            _questionCategoryService = questionCategoryService;
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
            var result=_questionDal.Add(question);
            return new SuccessDataResult<Question>(result, _messages.QuestionAdded);
        }

        [ValidationAspect(typeof(QuestionValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IQuestionService.Get")]
        public IResult AddWithDetails(QuestionDetailsDto question)
        {
            var addedQuestion = new Question
            {
                QuestionId = question.QuestionId,
                BrokenQuestion = question.BrokenQuestion,
                Privacy = question.Privacy,
                QuestionText = question.QuestionText,
                StarQuestion = question.StarQuestion,
                UserId = question.UserId,
            };
            question.QuestionId = this.AddWithId(addedQuestion).Data.QuestionId;

            AddRelations(question);
            return new SuccessResult(_messages.QuestionAdded);
        }



        [TransactionScopeAspect]
        private void AddRelations(QuestionDetailsDto question)
        {
            foreach (var option in question.Options)
            {
                option.QuestionId = question.QuestionId;
                _optionService.Add(option);
                Thread.Sleep(100);
            }

            foreach (var category in question.Categories)
            {
                _questionCategoryService.Add(new QuestionCategory
                {
                    CategoryId = category.CategoryId,
                    QuestionId = question.QuestionId
                });
                Thread.Sleep(100);
            }
        }

        [ValidationAspect(typeof(QuestionValidator))]
        [TransactionScopeAspect]
        [SecuredOperation("instructor")]
        [CacheRemoveAspect("IQuestionService.Get")]
        public IResult UpdateWithDetails(QuestionDetailsDto question)
        {
            var updatedQuestion = new Question
            {
                QuestionId = question.QuestionId,
                BrokenQuestion = question.BrokenQuestion,
                Privacy = question.Privacy,
                QuestionText = question.QuestionText,
                StarQuestion = question.StarQuestion,
                UserId = question.UserId,
            };
            _questionDal.Update(updatedQuestion);

            UpdateRelations(question);
            return new SuccessResult(_messages.QuestionUpdated);
        }

        [TransactionScopeAspect]
        private void UpdateRelations(QuestionDetailsDto question)
        {
            var defaultOptions = _optionService.GetAllByQuestionId(question.QuestionId).Data;
            var defaultCategories = _questionCategoryService.GetCategoriesByQuestionId(question.QuestionId).Data;

            foreach (var option in defaultOptions)
            {
                if (!question.Options.Any(o => o.Id == option.Id))
                {
                    _optionService.Delete(option);
                }
            }

            foreach (var option in question.Options)
            {
                option.QuestionId = question.QuestionId;

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

            foreach (var category in defaultCategories)
            {
                if (!question.Categories.Any(c => c.CategoryId == category.CategoryId))
                {
                    _questionCategoryService.Delete(category);
                }
            }

            foreach (var category in question.Categories)
            {
                var addedQuestionCategory = new QuestionCategory
                {
                    CategoryId = category.CategoryId,
                    QuestionId = question.QuestionId
                };
                var exists = _questionCategoryService.Get(addedQuestionCategory.Id).Data;
                if (exists == null)
                {
                    _questionCategoryService.Add(addedQuestionCategory);
                }
                else
                {
                    addedQuestionCategory.Id = exists.Id;
                    _questionCategoryService.Update(addedQuestionCategory);
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
            var deletedCategories = _questionCategoryService.GetCategoriesByQuestionId(question.QuestionId).Data;
            var deletedTestQuestions = _testQuestionService.GetTestQuestionsByQuestionId(question.QuestionId).Data;

            foreach (var option in deletedOptions)
            {
                _optionService.Delete(option);
            }
            foreach (var category in deletedCategories)
            {
                _questionCategoryService.Delete(category);
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
        public IDataResult<List<Question>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Question>>(_questionDal.GetQuestionsByCategoryId(categoryId));
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

        [CacheAspect(duration: 10)]
        public IDataResult<QuestionCategoriesDto> GetQuestionCategories(int questionId)
        {
            return new SuccessDataResult<QuestionCategoriesDto>(_questionDal.GetQuestionCategories(questionId));
        }

        public IDataResult<List<Question>> GetQuestionsByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Question>>(_questionDal.GetQuestionsByCategoryId(categoryId));
        }

        public IDataResult<List<QuestionDetailsDto>> GetDetailsByQuestionText(string text)
        {
            return new SuccessDataResult<List<QuestionDetailsDto>>(_questionDal.GetDetailsByQuestionText(text));
        }

        public IDataResult<Question> Get(int id)
        {
            return new SuccessDataResult<Question>(_questionDal.Get(q => q.QuestionId == id));

        }

        public IDataResult<List<QuestionDetailsDto>> GetDetailsByCategory(int categoryId)
        {
            return new SuccessDataResult<List<QuestionDetailsDto>>(_questionDal.GetDetailsByCategory(categoryId));
        }

        [CacheAspect(duration: 10)]
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

        public IDataResult<List<QuestionDetailsDto>> GetDetailsByUserWithCategory(int userId, int categoryId)
        {
            return new SuccessDataResult<List<QuestionDetailsDto>>(_questionDal.GetDetailsByCategory(categoryId)
                .Where(q => q.UserId == userId)
                .ToList());
        }
    }
}
