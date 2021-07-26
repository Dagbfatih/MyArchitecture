using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, SqlContext>,
        IUserOperationClaimDal
    {
        public List<UserOperationClaimDetailsDto> GetAllDetails()
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from uoc in context.UserOperationClaims
                             join oc in context.OperationClaims
                             on uoc.OperationClaimId equals oc.Id
                             join u in context.Users
                             on uoc.UserId equals u.Id
                             select new UserOperationClaimDetailsDto
                             {
                                 UserOperationClaimId = uoc.Id,
                                 OperationClaim = oc,
                                 User = u
                             };
                return result.ToList();
            }
        }

        public List<UserOperationClaimDetailsDto> GetAllDetailsByUser(int userId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from uoc in context.UserOperationClaims
                             join oc in context.OperationClaims
                             on uoc.OperationClaimId equals oc.Id
                             join u in context.Users
                             on uoc.UserId equals u.Id
                             where uoc.UserId == userId
                             select new UserOperationClaimDetailsDto
                             {
                                 UserOperationClaimId = uoc.Id,
                                 OperationClaim = oc,
                                 User = u
                             };
                return result.ToList();
            }
        }
    }
}
