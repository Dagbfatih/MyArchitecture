using Business.Abstract;
using Core.Entities.Concrete;
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
    public class RefreshTokensController : ControllerRepositoryBase<RefreshToken>
    {
        private IRefreshTokenService _refreshTokenService;
        public RefreshTokensController(IRefreshTokenService refreshTokenService) : base(refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }
    }
}
