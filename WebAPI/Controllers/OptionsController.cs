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
    public class OptionsController : ControllerBase
    {
        IOptionService _optionService;
        public OptionsController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpPost("add")]
        public IActionResult Add(Option option)
        {
            var result = _optionService.Add(option);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Option option)
        {
            var result = _optionService.Update(option);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _optionService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            Option option = _optionService.Get(id).Data;
            var result = _optionService.Delete(option);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
