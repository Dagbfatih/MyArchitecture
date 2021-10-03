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
    public class GradeLevelsController : ControllerBase
    {
        private readonly IGradeLevelService _gradeLevelService;

        public GradeLevelsController(IGradeLevelService gradeLevelService)
        {
            _gradeLevelService = gradeLevelService;
        }

        [HttpPost("add")]
        public IActionResult Add(GradeLevel gradeLevel)
        {
            var result = _gradeLevelService.Add(gradeLevel);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(GradeLevel gradeLevel)
        {
            var result = _gradeLevelService.Update(gradeLevel);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _gradeLevelService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(GradeLevel gradeLevel)
        {
            var result = _gradeLevelService.Delete(gradeLevel);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
