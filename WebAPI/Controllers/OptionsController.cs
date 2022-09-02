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
    public class OptionsController : ControllerRepositoryBase<Option>
    {
        private readonly IOptionService _optionService;
        public OptionsController(IOptionService optionService) : base(optionService)
        {
            _optionService = optionService;
        }   

        [HttpGet("getallbyquestion")]
        public IActionResult GetAllByQuestion(int questionId)
        {
            var result = _optionService.GetAllByQuestionId(questionId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
