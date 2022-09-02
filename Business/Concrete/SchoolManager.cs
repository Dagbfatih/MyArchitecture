using Business.Abstract;
using Core.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class SchoolManager : ISchoolService
    {

        private ISchoolDal _schoolDal;

        public SchoolManager(ISchoolDal schoolDal)
        {
            _schoolDal = schoolDal;
        }

        public IResult Add(School entity)
        {
            var result = BusinessRules.Run(CheckIfSchoolExists(entity));
            if (result != null)
            {
                return new ErrorResult();
            }
            _schoolDal.Add(entity);
            return new SuccessResult();
        }

        private IResult CheckIfSchoolExists(School entity)
        {
            if (_schoolDal.Get(s => s.UserId == entity.UserId) == null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Delete(School entity)
        {
            _schoolDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<School> Get(int id)
        {
            return new SuccessDataResult<School>(_schoolDal.Get(s => s.Id == id));
        }

        public IDataResult<List<School>> GetAll()
        {
            return new SuccessDataResult<List<School>>(_schoolDal.GetAll());
        }

        public IDataResult<School> GetByUser(int userId)
        {
            return new SuccessDataResult<School>(_schoolDal.Get(s => s.UserId == userId));
        }

        public IResult Update(School entity)
        {
            _schoolDal.Update(entity);
            return new SuccessResult();
        }
    }
}
