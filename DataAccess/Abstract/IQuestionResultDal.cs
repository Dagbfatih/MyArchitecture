using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IQuestionResultDal:IEntityRepository<QuestionResult>
    {
        List<QuestionResultDetailsDto> GetAllDetails();

        List<QuestionResultDetailsDto> GetAllDetailsByTestResultId(int testResultId);
    }
}
