using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService : IBusinessService<User>
    {
        int AddWithId(User user);
        List<OperationClaim> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
        IResult UpdateWithoutPassword(User user);
    }
}
