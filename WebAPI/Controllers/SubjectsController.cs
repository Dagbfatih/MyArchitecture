using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
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
    public class SubjectsController : ControllerRepositoryBase<Subject>
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService) : base(subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("getallbylesson")]
        [AllowAnonymous]
        public IActionResult GetAllByLesson(int lessonId)
        {
            var result = _subjectService.GetAllByLesson(lessonId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbylessons")]
        [AllowAnonymous]
        public IActionResult GetAllByIds([FromQuery(Name = "lessonIds")] int[] lessonIds)
        {
            var result = _subjectService.GetALlByLessons(lessonIds);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
