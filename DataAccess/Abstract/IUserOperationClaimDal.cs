using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IUserOperationClaimDal :IEntityRepository<UserOperationClaim>
    {
        List<UserOperationClaimDetailsDto> GetAllDetails();
        List<UserOperationClaimDetailsDto> GetAllDetailsByUser(int userId);
    }
}
