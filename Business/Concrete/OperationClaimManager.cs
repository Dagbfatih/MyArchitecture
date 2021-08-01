using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OperationClaimManager : BusinessMessagesService, IOperationClaimService
    {
        IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [SecuredOperation("admin")]
        public IResult Add(OperationClaim entity)
        {
            _operationClaimDal.Add(entity);
            return new SuccessResult(_messages.OperationClaimAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(OperationClaim entity)
        {
            _operationClaimDal.Delete(entity);
            return new SuccessResult(_messages.OperationClaimDeleted);
        }

        public IDataResult<OperationClaim> Get(int id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal
                .Get(o => o.Id == id));
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        [SecuredOperation("admin")]
        public IResult Update(OperationClaim entity)
        {
            _operationClaimDal.Update(entity);
            return new SuccessResult(_messages.OperationClaimUpdated);
        }
    }
}
