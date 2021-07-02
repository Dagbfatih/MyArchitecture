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
    public class RolesController : ControllerBase
    {
        IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("add")]
        public IActionResult Add(Role role)
        {
            var result = _roleService.Add(role);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Role role)
        {
            var result = _roleService.Delete(role);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _roleService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("get")]
        public IActionResult GetAll(int id)
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
