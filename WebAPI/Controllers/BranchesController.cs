using Business.Abstract;
using Entities.Concrete;
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
    public class BranchesController : ControllerRepositoryBase<Branch>
    {
        IBranchService _branchService;
        public BranchesController(IBranchService branchService) : base(branchService)
        {
            _branchService = branchService;
        }
    }
}
