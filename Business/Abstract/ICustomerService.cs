using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService : IBusinessService<Customer>
    {
        IDataResult<Customer> GetByUser(int userId);
        IDataResult<CustomerDetailsDto> GetCustomerDetailsByUser(int userId);
        IResult ConfirmAccount(Customer customer);
    }
}
