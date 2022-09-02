using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IQuestionService : IBusinessServiceRepository<Question>
    {
        IDataResult<List<QuestionDetailsDto>> GetAllDetailsByPublic();
        IResult AddWithDetails(QuestionDetailsDto question);
        IResult DeleteWithDetails(QuestionDetailsDto question);
        IDataResult<List<Question>> GetAllByStarQuestion();
        IDataResult<List<Question>> GetAllByOptionName(string optionName);
        IDataResult<List<Question>> GetAllByOptionNumber(int optionNumber);
        IDataResult<QuestionDetailsDto> GetQuestionDetailsById(int questionId);
        IDataResult<List<QuestionDetailsDto>> GetQuestionsDetails();
        IResult AddTransactionalOperation(Question question);
        IDataResult<List<QuestionDetailsDto>> GetDetailsByQuestionText(string text);
        IDataResult<List<QuestionDetailsDto>> GetDetailsByUser(int userId);
        IResult UpdateWithDetails(QuestionDetailsDto question);
        IDataResult<Question> AddWithId(Question question);
        IDataResult<List<QuestionDetailsDto>> GetAllDetailsBySubjectsAndPublic(params int[] subjects);

    }
}
