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
    public class LessonsController : ControllerRepositoryBase<Lesson>
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService) : base(lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("[action]")]
        public IActionResult GetAllByGradeLevel(int gradeLevelId)
        {
            var result = _lessonService.GetAllByGradeLevel(gradeLevelId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
