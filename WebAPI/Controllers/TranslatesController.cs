using Business.Abstract;
using Core.Entities.Concrete;
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
    [AllowAnonymous]
    public class TranslatesController : ControllerBase
    {
        ITranslateService _translateService;
        public TranslatesController(ITranslateService translateService)
        {
            _translateService =translateService;
        }

        [HttpPost("add")]
        public IActionResult Add(Translate translate)
        {
            var result = _translateService.Add(translate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Translate translate)
        {
            var result = _translateService.Update(translate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Translate translate)
        {
            var result = _translateService.Delete(translate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _translateService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbycode")]
        public IActionResult GetAllByCode(string code)
        {
            var result = _translateService.GetAllByCode(code);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbylanguage")]
        public IActionResult GetAllByCode(int languageId)
        {
            var result = _translateService.GetAllByLanguage(languageId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldetails")]
        public IActionResult GetAllDetails()
        {
            var result = _translateService.GetAllDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getalldetailsbycode")]
        public IActionResult GetAllDetailsByCode(string code)
        {
            var result = _translateService.GetAllDetailsByCode(code);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldetailsbylanguage")]
        public IActionResult GetAllDetailsByLanguage(int languageId)
        {
            var result = _translateService.GetAllDetailsByLanguage(languageId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
