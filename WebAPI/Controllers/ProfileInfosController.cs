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
    public class ProfileInfosController : ControllerRepositoryBase<ProfileInfo>
    {
        private IProfileInfoService _profileInfoService;

        public ProfileInfosController(IProfileInfoService profileInfoService) : base(profileInfoService)
        {
            _profileInfoService = profileInfoService;
        }

        [HttpGet("getalldetails")]
        public IActionResult GetAllDetails()
        {
            var result = _profileInfoService.GetAllDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdetailsbyuser")]
        public IActionResult GetDetailsByUser(int userId)
        {
            var result = _profileInfoService.GetDetailsByUser(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[Action]")]
        public IActionResult UpdateInfos(ProfileInfoForUpdate profileInfo)
        {
            var result = _profileInfoService.UpdateInfos(profileInfo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
