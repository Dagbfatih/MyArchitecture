using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
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
    public class TestsController : ControllerBase
    {
        ITestService _testService;
        public TestsController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpPost("add")]
        public IActionResult Add(Test test)
        {
            var result = _testService.Add(test);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
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

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _testService.GetAll();
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

        [HttpDelete("delete")]
        public IActionResult Delete(Test test)
        {
            var result = _testService.Delete(test);
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
