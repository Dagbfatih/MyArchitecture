using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBusinessService<T>
    {
        IResult Add(T entity);
        IResult Delete(T entity);
        IResult Update(T entity);
        IDataResult<List<T>> GetAll();
        IDataResult<T> Get(int id);

    }
}
