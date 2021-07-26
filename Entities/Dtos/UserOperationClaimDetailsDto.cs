using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserOperationClaimDetailsDto : IDto
    {
        public int UserOperationClaimId { get; set; }
        public User User { get; set; }
        public OperationClaim OperationClaim { get; set; }
    }
}
