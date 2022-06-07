using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IOperationClaimDal : IEntityRepository<OperationClaim>
    {
    }
}
