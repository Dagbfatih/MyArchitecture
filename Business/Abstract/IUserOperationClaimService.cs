using Core.Business;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserOperationClaimService : IBusinessServiceRepository<UserOperationClaim>
    {
        IResult AddClaims(List<UserOperationClaim> claims);
        IDataResult<List<UserOperationClaimDetailsDto>> GetAllDetails();
        IDataResult<List<UserOperationClaimDetailsDto>> GetAllDetailsByUser(int userId);
    }
}
