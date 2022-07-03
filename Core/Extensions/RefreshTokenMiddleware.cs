using Core.Utilities.Exceptions;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class RefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private JwtSecurityTokenHandler _tokenHandler;
        IConfiguration _configuration;
        public RefreshTokenMiddleware(RequestDelegate next,
            JwtSecurityTokenHandler tokenHandler,
            IConfiguration configuration)
        {
            _next = next;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
            var claims = httpContext.User.Claims.ToList();
            if (!httpContext.Request.Path.Value.Contains("auth/refreshtoken"))
            {
                await CheckExpiration(httpContext, token, tokenOptions, claims);
            }

            await _next(httpContext);
        }


        public async Task CheckExpiration(HttpContext httpContext, string token, TokenOptions tokenOptions,
            List<Claim> claims)
        {
            if (token != null && token != "null")
            {
                _tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
            }

            await _next(httpContext);
        }
    }
}
