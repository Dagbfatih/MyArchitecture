using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOptionService : IBusinessServiceRepository<Option>
    {
        IDataResult<List<Option>> GetAllByQuestionId(int questionId);
        IDataResult<Option> GetOptionByCorrectOption(int questionId);
    }
}
