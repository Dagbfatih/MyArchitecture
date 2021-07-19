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
    public class QuestionResultsController : ControllerBase
    {
        IQuestionResultService _questionResultService;
        public QuestionResultsController(IQuestionResultService questionResultService)
        {
            _questionResultService = questionResultService;
        }

        [HttpPost("add")]
        public IActionResult Add(QuestionResult questionResult)
        {
            var result = _questionResultService.Add(questionResult);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(QuestionResult questionResult)
        {
            var result = _questionResultService.Update(questionResult);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _questionResultService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldetails")]
        public IActionResult GetAllDetails()
        {
            var result = _questionResultService.GetAllDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpGet("getalldetailsbytestresultid")]
        public IActionResult GetAllDetailsByTestResultId(int testResultId)
        {
            var result = _questionResultService.GetAllDetailsByTestResultId(testResultId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
