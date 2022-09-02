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
    public class LanguagesController : ControllerRepositoryBase<Language>
    {
        ILanguageService _languageService;
        public LanguagesController(ILanguageService languageService) : base(languageService)
        {
            _languageService = languageService;
        }

        [HttpGet("getbycode")]
        public IActionResult GetByCode(string code)
        {
            var result = _languageService.GetLanguageByCode(code);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
