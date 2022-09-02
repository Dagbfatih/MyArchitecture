using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITestService : IBusinessServiceRepository<Test>
    {
        IResult AddWithDetails(TestDetailsDto testDetailsDto);
        IDataResult<int> AddWithId(Test test);
        IDataResult<List<TestDetailsDto>> GetTestDetails();
        IDataResult<TestDetailsDto> GetTestDetailsById(int id);
        IDataResult<List<TestDetailsDto>> GetTestDetailsByUser(int userId);
        IResult UpdateWithDetails(TestDetailsDto testDetailsDto);
        IResult DeleteWithDetails(TestDetailsDto testDetailsDto);
        IDataResult<List<TestDetailsDto>> GetTestDetailsByPublic();

    }
}
