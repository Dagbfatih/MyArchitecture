using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRefreshTokenService : IBusinessServiceRepository<RefreshToken>
    {
        IResult DeleteIfExists(User user);
        IDataResult<RefreshToken> GetByRefreshToken(string refreshToken);
        IDataResult<RefreshToken> GenerateRefreshToken(User user);
        IDataResult<RefreshToken> GetByUser(int userId);

    }
}
