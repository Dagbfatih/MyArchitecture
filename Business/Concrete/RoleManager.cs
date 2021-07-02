using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RoleManager : IRoleService
    {
        IRoleDal _roleDal;

        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        public IResult Add(Role entity)
        {
            _roleDal.Add(entity);
            return new SuccessResult(Messages.RoleAdded);
        }

        public IResult Delete(Role entity)
        {
            _roleDal.Delete(entity);
            return new SuccessResult(Messages.RoleDeleted);
        }

        public IDataResult<Role> Get(int id)
        {
            return new SuccessDataResult<Role>(_roleDal.Get(r => r.Id == id));
        }

        public IDataResult<List<Role>> GetAll()
        {
            return new SuccessDataResult<List<Role>>(_roleDal.GetAll());
        }

        public IResult Update(Role entity)
        {
            _roleDal.Update(entity);
            return new SuccessResult(Messages.RoleUpdated);
        }
    }
}
