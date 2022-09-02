using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITestQuestionService : IBusinessServiceRepository<TestQuestion>
    {
        IDataResult<List<TestQuestion>> GetTestQuestionsByQuestionId(int questionId);
        IDataResult<List<TestQuestion>> GetAllByTest(int testId);
        IDataResult<TestQuestion> GetAllByTestAndQuestion(int testId, int questionId);
    }
}
