using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService : IBusinessServiceRepository<User>
    {
        int AddWithId(User user);
        List<OperationClaim> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<User>> GetAllByIds(params int[] userIds);
        IResult UpdateWithoutPassword(User user);
    }
}
