using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService : IBusinessServiceRepository<Customer>
    {
        IDataResult<Customer> GetByUser(int userId);
        IDataResult<CustomerDetailsDto> GetCustomerDetailsByUser(int userId);
        IDataResult<List<CustomerDetailsDto>> GetAllByUserIds(params int[] userIds);
        IResult ConfirmAccount(Customer customer);
    }
}
