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
    [Route("api/[controller]")] // [Route("api/[controller]")] demek: kullanıcının bize (servera, yani api'ye) nasıl erişileceğini söyler. Yani parantez içerisinde önce https://localhost:44389/ yazdıktan sonra (route içerisinde görüldüğü gibi) 'api/questions' yazarak bu apiye erişilebilir. Eğer istenirse bu değiştirilebilir. 'https://localhost:44389/api/questions'
    // sadece questions yazmak yeterlidir çünkü WebAPI bizim için QuestionsController isminden controller'i keser. Domain adresi yerine sadece 'questions' yazmak yeterli olur.

    [ApiController]
    public class QuestionsController : ControllerBase
    {
        IQuestionService _questionService;
        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _questionService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldetailsbypublic")]
        [AllowAnonymous]
        public IActionResult GetAllDetailsByPublic()
        {
            var result = _questionService.GetAllDetailsByPublic();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Question question)
        {
            var result = _questionService.Add(question);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addwithrelations")]
        public IActionResult AddWithRelations(QuestionDetailsDto question)
        {
            var result = _questionService.AddWithDetails(question);

            return Ok(result);
        }

        [HttpPut("updatewithrelations")]
        public IActionResult UpdateWithRelations(QuestionDetailsDto question)
        {
            var result = _questionService.UpdateWithDetails(question);

            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Question question)
        {
            var result = _questionService.Delete(question);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Question question)
        {
            var result = _questionService.Update(question);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyoptionname")]
        public IActionResult GetAllByOptionName(string optionName)
        {
            var result = _questionService.GetAllByOptionName(optionName);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbyoptionnumber")]
        public IActionResult GetAllByOptionNumber(int optionNumber)
        {
            var result = _questionService.GetAllByOptionNumber(optionNumber);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbystarquestion")]
        public IActionResult GetAllByStarQuestion()
        {
            var result = _questionService.GetAllByStarQuestion();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldetails")]
        public IActionResult GetQuestionDetails()
        {
            var result = _questionService.GetQuestionsDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdetailsbyid")]
        public IActionResult GetQuestionDetailsById(int questionId)
        {
            var result = _questionService.GetQuestionDetailsById(questionId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdetailsbyuser")]
        public IActionResult GetDetailsByUser(int userId)
        {
            var result = _questionService.GetDetailsByUser(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
