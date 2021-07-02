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
    public class QuestionCategoriesController : ControllerBase
    {
        IQuestionCategoryService _questionCategoryService;

        public QuestionCategoriesController(IQuestionCategoryService questionCategoryService)
        {
            _questionCategoryService = questionCategoryService;
        }

        [HttpPost("add")]
        public IActionResult Add(QuestionCategory questionCategory)
        {
            var result = _questionCategoryService.Add(questionCategory);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(QuestionCategory questionCategory)
        {
            var result = _questionCategoryService.Update(questionCategory);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _questionCategoryService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            QuestionCategory questionCategory = _questionCategoryService.Get(id).Data;
            var result = _questionCategoryService.Delete(questionCategory);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
