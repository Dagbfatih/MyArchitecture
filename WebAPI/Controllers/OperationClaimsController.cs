using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerRepositoryBase<OperationClaim>
    {
        IOperationClaimService _operationClaimService;
        public OperationClaimsController(IOperationClaimService operationClaimService) : base(operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }
    }
}
