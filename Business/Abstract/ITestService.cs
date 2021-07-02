﻿using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITestService : IBusinessService<Test>
    {
        IResult AddWithDetails(TestDetailsDto testDetailsDto);
        IDataResult<List<TestDetailsDto>> GetTestDetails();
        IDataResult<TestDetailsDto> GetTestDetailsById(int testId);
        
    }
}
