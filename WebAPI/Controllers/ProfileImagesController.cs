using Business.Abstract;
using Entities.Concrete;
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
    public class ProfileImagesController : ControllerBase
    {
        IProfileImageService _profileImageService;
        public ProfileImagesController(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("Image"))] IFormFile formFile, [FromForm] ProfileImage profileImage)
        {
            var result = _profileImageService.Add(profileImage, formFile);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            ProfileImage carImage = _profileImageService.Get(id).Data;
            var result = _profileImageService.Delete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = "Image")] IFormFile formFile, [FromForm(Name = "Id")] int id)
        {
            ProfileImage profileImage = _profileImageService.Get(id).Data;
            var result = _profileImageService.Update(profileImage, formFile);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _profileImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _profileImageService.GetImageByUserId(userId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbyusers")]
        public IActionResult GetAllByUsers([FromQuery(Name ="userIds")]int[] userIds)
        {
            var result = _profileImageService.GetImagesByUsers(userIds);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
