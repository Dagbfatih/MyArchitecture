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
    public class TestResultsController : ControllerRepositoryBase<TestResult>
    {
        private readonly ITestResultService _testResultService;

        public TestResultsController(ITestResultService testResultService) : base(testResultService)
        {
            _testResultService = testResultService;
        }

        [HttpPost("addwithdetails")]
        public IActionResult AddWithDetails(TestResultDetailsDto testResult)
        {
            var result = _testResultService.AddWithDetails(testResult);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldetails")]
        public IActionResult GetAllDetails()
        {
            var result = _testResultService.GetAllDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldetailsbyuser")]
        public IActionResult GetAllDetailsByUser(int userId)
        {
            var result = _testResultService.GetAllDetailsByUser(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdetailsbyid")]
        public IActionResult GetDetailsById(int id)
        {
            var result = _testResultService.GetDetailsById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
