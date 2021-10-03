using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : BusinessMessagesService, ICustomerService
    {
        ICustomerDal _customerDal;
        IUserOperationClaimService _userOperationClaimService;

        public CustomerManager(ICustomerDal customerDal,
            IUserOperationClaimService userOperationClaimService)
        {
            _customerDal = customerDal;
            _userOperationClaimService = userOperationClaimService;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer entity)
        {
            var result = BusinessRules.Run(CheckIfUserExists(entity));

            if (result != null)
            {
                return result;
            }

            _customerDal.Add(entity);
            return new SuccessResult(_messages.CustomerAdded);
        }

        private IResult CheckIfUserExists(Customer customer)
        {
            var result = this.GetByUser(customer.UserId);
            if (result.Data != null)
            {
                return new ErrorResult(_messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IResult Delete(Customer entity)
        {
            _customerDal.Delete(entity);
            return new SuccessResult(_messages.CustomerDeleted);
        }

        public IDataResult<Customer> Get(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.Id == id));
        }

        [SecuredOperation("user, instructor, student", true)]
        public IDataResult<Customer> GetByUser(int userId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == userId));
        }

        [SecuredOperation("admin")]
        [PerformanceAspect(5)]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer entity)
        {
            _customerDal.Update(entity);
            return new SuccessResult(_messages.CustomerUpdated);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin")]
        public IResult ConfirmAccount(Customer entity)
        {
            entity.IsConfirmed = true;

            _customerDal.Update(entity);
            _userOperationClaimService.Add(new UserOperationClaim
            {
                OperationClaimId = 3,
                UserId = entity.UserId
            });

            return new SuccessResult(_messages.AccountConfirmed);
        }

        [SecuredOperation("user, admin, instructor, student", true)]
        [PerformanceAspect(5)]
        public IDataResult<CustomerDetailsDto> GetCustomerDetailsByUser(int userId)
        {
            return new SuccessDataResult<CustomerDetailsDto>(_customerDal.GetCustomerDetailsByUser(userId));
        }

        public IDataResult<List<CustomerDetailsDto>> GetAllByUserIds(params int[] userIds)
        {
            List<CustomerDetailsDto> result = new List<CustomerDetailsDto>();

            foreach (var userId in userIds)
            {
                var customer = GetCustomerDetailsByUser(userId).Data;
                if (customer != null)
                {
                    result.Add(customer);
                }
            }
            return new SuccessDataResult<List<CustomerDetailsDto>>(result);
        }
    }
}
