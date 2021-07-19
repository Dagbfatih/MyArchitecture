using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ITestResultDal : IEntityRepository<TestResult>
    {
        List<TestResultDetailsDto> GetAllDetails();
        List<TestResultDetailsDto> GetAllDetailsByUser(int userId);
        TestResultDetailsDto GetDetailsById(int id);

    }
}
