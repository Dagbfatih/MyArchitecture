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
    public class TestQuestionsController : ControllerBase
    {
        ITestQuestionService _testQuestionService;
        public TestQuestionsController(ITestQuestionService testQuestionService) 
        {
            _testQuestionService = testQuestionService;
        }

        [HttpPost("add")]
        public IActionResult Add(TestQuestion testQuestion)
        {
            var result = _testQuestionService.Add(testQuestion);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _testQuestionService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
