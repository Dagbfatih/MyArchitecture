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
    public class QuestionTicketsController : ControllerBase
    {
        IQuestionTicketService _questionTicketService;
        public QuestionTicketsController(IQuestionTicketService questionTicketService)
        {
            _questionTicketService = questionTicketService;
        }

        [HttpPost("add")]
        public IActionResult Add(QuestionTicket questionTicket)
        {
            var result = _questionTicketService.Add(questionTicket);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _questionTicketService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(QuestionTicket questionTicket)
        {
            var result = _questionTicketService.Delete(questionTicket);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
