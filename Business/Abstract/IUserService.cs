using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService : IBusinessService<User>
    {
        List<OperationClaim> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
    }
}
