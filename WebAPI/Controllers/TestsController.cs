using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
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
    public class TestsController : ControllerRepositoryBase<Test>
    {
        ITestService _testService;
        public TestsController(ITestService testService) : base(testService)
        {
            _testService = testService;
        }

        [HttpPost("addwithdetails")]
        public IActionResult AddWithDetails(TestDetailsDto test)
        {
            var result = _testService.AddWithDetails(test);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("updatewithdetails")]
        public IActionResult UpdateWithDetails(TestDetailsDto test)
        {
            var result = _testService.UpdateWithDetails(test);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("deletewithdetails")]
        public IActionResult DeleteWithDetails(TestDetailsDto test)
        {
            var result = _testService.DeleteWithDetails(test);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdetails")]
        public IActionResult GetDetails()
        {
            var result = _testService.GetTestDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("gettestdetailsbyuser")]
        public IActionResult GetTestDetailsByUser(int userId)
        {
            var result = _testService.GetTestDetailsByUser(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("gettestdetailsbypublic")]
        [AllowAnonymous]
        public IActionResult GetTestDetailsByPublic()
        {
            var result = _testService.GetTestDetailsByPublic();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("gettestdetailsbyid")]
        public IActionResult GetTestDetailsById(int id)
        {
            var result = _testService.GetTestDetailsById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
