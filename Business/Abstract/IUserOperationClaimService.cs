using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserOperationClaimService: IBusinessServiceRepository<UserOperationClaim>
    {
        IResult AddClaims(List<UserOperationClaim> claims);
        IDataResult<List<UserOperationClaimDetailsDto>> GetAllDetails();
        IDataResult<List<UserOperationClaimDetailsDto>> GetAllDetailsByUser(int userId);
    }
}
