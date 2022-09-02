using Core.Entities.Abstract;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business
{
    public interface IBusinessServiceRepository<T> where T : class, IEntity, new()
    {
        IResult Add(T entity);
        IResult Delete(T entity);
        IResult Update(T entity);
        IDataResult<T> Get(int id);
        IDataResult<List<T>> GetAll();
    }
}
