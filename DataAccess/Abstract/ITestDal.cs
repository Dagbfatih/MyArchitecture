using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ITestDal : IEntityRepository<Test>
    {
        List<TestDetailsDto> GetTestDetails();
        TestDetailsDto GetTestDetailsById(int id);
        List<TestDetailsDto> GetTestDetailsByUser(int userId);
    }
}
