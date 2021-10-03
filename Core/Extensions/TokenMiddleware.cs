using Core.Entities.Concrete;
using Core.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class TokenMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public TokenMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var claims = httpContext.User.Claims.ToList();
            var user = new User();

            if (claims.Count > 0)
            {
                user = new User
                {
                    Id = Int32.Parse(claims.Find(c => c.Type == ClaimTypes.NameIdentifier).Value),
                    FirstName = claims.Find(c => c.Type == ClaimTypes.Name).Value,
                    LastName = claims.Find(c => c.Type == "lastname").Value,
                    Email = claims.Find(c => c.Type == ClaimTypes.Email).Value,
                    Status = claims.Find(c => c.Type == CustomClaimTypes.Status)
                .Value == "true"
                };
            }
            else
            {
                user = null;
            }

            _tokenService.SetUser(user);

            await _next(httpContext);
        }
    }
}
