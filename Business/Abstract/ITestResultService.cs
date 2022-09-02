using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ITestResultService : IBusinessServiceRepository<TestResult>
    {
        IDataResult<List<TestResultDetailsDto>> GetAllDetails();
        IDataResult<List<TestResultDetailsDto>> GetAllDetailsByUser(int userId);
        IDataResult<TestResultDetailsDto> GetDetailsById(int id);

        IResult AddWithDetails(TestResultDetailsDto testResult);

    }
}
