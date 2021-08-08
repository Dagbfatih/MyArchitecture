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
    public class BranchManager : BusinessMessagesService, IBranchService
    {
        IBranchDal _branchDal;
        public BranchManager(IBranchDal branchDal)
        {
            _branchDal = branchDal;
        }

        [SecuredOperation("admin")]
        public IResult Add(Branch entity)
        {
            _branchDal.Add(entity);
            return new SuccessResult(_messages.BranchAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Branch entity)
        {
            _branchDal.Delete(entity);
            return new SuccessResult(_messages.BranchDeleted);
        }

        public IDataResult<Branch> Get(int id)
        {
            return new SuccessDataResult<Branch>(_branchDal.Get(b => b.Id == id));
        }

        public IDataResult<List<Branch>> GetAll()
        {
            return new SuccessDataResult<List<Branch>>(_branchDal.GetAll());
        }

        [SecuredOperation("admin")]
        public IResult Update(Branch entity)
        {
            _branchDal.Update(entity);
            return new SuccessResult(_messages.BranchUpdated);
        }
    }
}
