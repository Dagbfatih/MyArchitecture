using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Core.Entities.Dtos;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IUserOperationClaimService _userOperationClaimService;
        private IRefreshTokenService _refreshTokenService;
        private IUserService _userService;

        public AuthController(
            IAuthService authService,
            IUserOperationClaimService userOperationClaimService,
            IRefreshTokenService refreshTokenService,
            IUserService userService)
        {
            _authService = authService;
            _userOperationClaimService = userOperationClaimService;
            _refreshTokenService = refreshTokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            if (!registerResult.Success)
            {
                return BadRequest(registerResult);
            }

            var result = _authService.CreateToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("registerwithcustomer")]
        [AllowAnonymous]
        public IActionResult RegisterWithCustomer(CustomerForRegisterDto customerForRegisterDto)
        {
            var registerResult = _authService.RegisterWithCustomer(
                customerForRegisterDto.User,
                customerForRegisterDto.User.Password,
                customerForRegisterDto.Customer);

            if (!registerResult.Success)
            {
                return BadRequest(registerResult);
            }

            var result = _authService.CreateToken(registerResult.Data);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(customerForRegisterDto);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult RefreshToken()
        {
            string clientRefreshToken = HttpContext.Request.Headers["refreshToken"];
            var refreshTokenIsValid = _authService.RefreshTokenIsValid(clientRefreshToken);

            if (!refreshTokenIsValid.Success) // Token is changed or invalid
            {
                return BadRequest(refreshTokenIsValid);
            }

            RefreshToken newRefreshToken;
            var storedRefreshToken = _refreshTokenService.GetByRefreshToken(clientRefreshToken);
            var user = _userService.Get(storedRefreshToken.Data.UserId);

            if (_authService.RefreshTokenExpired(clientRefreshToken).Success) // Token is expired
            {
                newRefreshToken = _authService.RefreshToken(user.Data).Data;
            }
            else
            {
                newRefreshToken = storedRefreshToken.Data;
            }

            var token = new Token
            {
                AccessToken = _authService.CreateAccessToken(user.Data).Data,
                RefreshToken = newRefreshToken
            };

            return Ok(new SuccessDataResult<Token>(token));
        }

        [HttpPost("logout")]
        public IActionResult Logout(User user)
        {
            var result = _refreshTokenService.DeleteIfExists(user);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
