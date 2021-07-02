using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IQuestionCategoryService : IBusinessService<QuestionCategory>
    {
        IDataResult<List<QuestionCategory>> GetCategoriesByQuestionId(int questionId);
    }
}
