﻿using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITestQuestionService : IBusinessService<TestQuestion>
    {
        IDataResult<List<TestQuestion>> GetTestQuestionsByQuestionId(int questionId);
        IResult TransactionalOperation(TestQuestion testQuestion);
    }
}
