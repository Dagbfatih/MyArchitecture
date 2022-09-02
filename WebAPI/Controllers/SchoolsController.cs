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
    public class SchoolsController : ControllerRepositoryBase<School>
    {
        private ISchoolService _schoolService;

        public SchoolsController(ISchoolService schoolService) : base(schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet("[action]")]
        public IActionResult GetByUser(int userId)
        {
            var result = _schoolService.GetByUser(userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
