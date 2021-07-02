using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IQuestionService : IBusinessService<Question>
    {
        IResult AddWithDetails(QuestionDetailsDto question);
        IDataResult<List<Question>> GetAllByCategoryId(int categoryId);
        IDataResult<List<Question>> GetAllByStarQuestion();
        IDataResult<List<Question>> GetAllByOptionName(string optionName);
        IDataResult<List<Question>> GetAllByOptionNumber(int optionNumber);
        IDataResult<QuestionDetailsDto> GetQuestionDetailsById(int questionId);
        IDataResult<List<QuestionDetailsDto>> GetQuestionsDetails();
        IDataResult<QuestionCategoriesDto> GetQuestionCategories(int questionId);
        IDataResult<List<Question>> GetQuestionsByCategoryId(int categoryId);
        IResult AddTransactionalOperation(Question question);
        IDataResult<List<QuestionDetailsDto>> GetDetailsByQuestionText(string text);
        IDataResult<List<QuestionDetailsDto>> GetDetailsByCategory(int categoryId);


    }
}
