using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService:IBusinessServiceRepository<Category>
    {
        IDataResult<List<Category>> GetAllByCategoryName(string categoryName);

    }
}
