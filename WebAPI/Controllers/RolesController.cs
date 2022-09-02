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
    public class RolesController : ControllerRepositoryBase<Role>
    {
        IRoleService _roleService;
        public RolesController(IRoleService roleService) : base(roleService)
        {
            _roleService = roleService;
        }
        
        [HttpGet("get")]
        public IActionResult Get(int id)
        {
            var result = _roleService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
