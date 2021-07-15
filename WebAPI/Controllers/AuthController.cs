using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Dtos;
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
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            if (!registerResult.Success)
            {
                return BadRequest(registerResult);
            }

            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("registerwithcustomer")]
        public ActionResult RegisterWithCustomer(CustomerForRegisterDto customerForRegisterDto)
        {
            var userForRegisterDto = new UserForRegisterDto
            {
                FirstName = customerForRegisterDto.FirstName,
                LastName = customerForRegisterDto.LastName,
                Email = customerForRegisterDto.Email,
                Password = customerForRegisterDto.Password
            };

            var customer = new Customer
            {
                RoleId = customerForRegisterDto.RoleId,
                UserId = customerForRegisterDto.UserId
            };

            var registerResult = _authService.RegisterWithCustomer(userForRegisterDto, userForRegisterDto.Password, customer);
            if (!registerResult.Success)
            {
                return BadRequest(registerResult);
            }

            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
