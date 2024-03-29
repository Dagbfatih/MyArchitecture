﻿using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ILanguageService : IBusinessServiceRepository<Language>
    {
        IDataResult<Language> GetLanguageByCode(string code);
    }
}
