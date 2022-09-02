using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IQuestionResultService: IBusinessServiceRepository<QuestionResult>
    {
        IDataResult<List<QuestionResultDetailsDto>> GetAllDetails();
        IDataResult<List<QuestionResultDetailsDto>> GetAllDetailsByTestResultId(int testResultId);

        IResult AddWithDetails(QuestionResultDetailsDto questionResult);
    }
}
