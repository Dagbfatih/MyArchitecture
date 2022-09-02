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
    public class QuestionResultsController : ControllerRepositoryBase<QuestionResult>
    {
        IQuestionResultService _questionResultService;
        public QuestionResultsController(IQuestionResultService questionResultService) : base(questionResultService)
        {
            _questionResultService = questionResultService;
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
