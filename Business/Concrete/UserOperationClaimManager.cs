using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserOperationClaimManager : BusinessMessagesService, IUserOperationClaimService
    {
        IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        public IResult Add(UserOperationClaim entity)
        {
            _userOperationClaimDal.Add(entity);
            return new SuccessResult(_messages.UserOperationClaimAdded);
        }

        public IResult AddClaims(List<UserOperationClaim> claims)
        {
            foreach (var userOperationClaim in claims)
            {
                this.Add(userOperationClaim);
            }

            return new SuccessResult(_messages.UserOperationClaimAdded);
        }

        public IResult Delete(UserOperationClaim entity)
        {
            _userOperationClaimDal.Delete(entity);
            return new SuccessResult(_messages.UserOperationClaimDeleted);
        }

        public IDataResult<UserOperationClaim> Get(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.
                Get(o => o.Id == id));
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal
                .GetAll());
        }

        public IDataResult<List<UserOperationClaimDetailsDto>> GetAllDetails()
        {
            return new SuccessDataResult<List<UserOperationClaimDetailsDto>>(
                _userOperationClaimDal.GetAllDetails());
        }

        public IDataResult<List<UserOperationClaimDetailsDto>> GetAllDetailsByUser(int userId)
        {
            return new SuccessDataResult<List<UserOperationClaimDetailsDto>>(
                _userOperationClaimDal.GetAllDetailsByUser(userId));
        }

        public IResult Update(UserOperationClaim entity)
        {
            _userOperationClaimDal.Update(entity);
            return new SuccessResult(_messages.UserOperationClaimUpdated);
        }
    }
}
