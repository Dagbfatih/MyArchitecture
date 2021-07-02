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
    public class TestTicketsController : ControllerBase
    {
        ITestTicketService _testTicketService;
        public TestTicketsController(ITestTicketService testTicketService)
        {
            _testTicketService = testTicketService;
        }

        [HttpPost("add")]
        public IActionResult Add(TestTicket testTicket)
        {
            var result = _testTicketService.Add(testTicket);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _testTicketService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(TestTicket testTicket)
        {
            var result = _testTicketService.Delete(testTicket);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
