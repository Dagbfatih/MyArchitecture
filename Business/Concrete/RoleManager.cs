using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
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
    public class RoleManager : BusinessMessagesService, IRoleService
    {
        IRoleDal _roleDal;
        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        [SecuredOperation("admin")]
        public IResult Add(Role entity)
        {
            _roleDal.Add(entity);
            return new SuccessResult(_messages.RoleAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Role entity)
        {
            _roleDal.Delete(entity);
            return new SuccessResult(_messages.RoleDeleted);
        }

        public IDataResult<Role> Get(int id)
        {
            return new SuccessDataResult<Role>(_roleDal.Get(r => r.Id == id));
        }

        public IDataResult<List<Role>> GetAll()
        {
            return new SuccessDataResult<List<Role>>(_roleDal.GetAll());
        }

        [SecuredOperation("admin")]
        public IResult Update(Role entity)
        {
            _roleDal.Update(entity);
            return new SuccessResult(_messages.RoleUpdated);
        }
    }
}
