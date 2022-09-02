using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Services;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CityManager : BusinessMessagesService, ICityService
    {
        private ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        [SecuredOperation("admin")]
        public IResult Add(City entity)
        {
            _cityDal.Add(entity);
            return new SuccessResult();
        }

        [SecuredOperation("admin")]
        public IResult Delete(City entity)
        {
            _cityDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<City> Get(int id)
        {
            return new SuccessDataResult<City>(_cityDal.Get(c => c.Id == id));
        }

        public IDataResult<List<City>> GetAll()
        {
            return new SuccessDataResult<List<City>>(_cityDal.GetAll());
        }

        [SecuredOperation("admin")]
        public IResult Update(City entity)
        {
            _cityDal.Update(entity);
            return new SuccessResult();
        }
    }
}
