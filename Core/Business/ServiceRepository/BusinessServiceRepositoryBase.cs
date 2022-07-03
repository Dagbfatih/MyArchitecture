using Core.Entities.Abstract;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.ServiceRepository
{
    public class BusinessServiceRepositoryBase<T> : IBusinessServiceRepository<T>
        where T : class, IEntity, new()
    {
        public IResult Add(T entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
