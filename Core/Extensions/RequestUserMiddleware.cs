﻿using Core.Business.Services;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class RequestUserMiddleware
    {
        private RequestDelegate _next;
        private IRequestUserService _requestUserService;
        public RequestUserMiddleware(RequestDelegate next, IRequestUserService requestUserService)
        {
            _next = next;
            _requestUserService = requestUserService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var claims = httpContext.User.Identities.First().Claims.ToList();
            var user = new User();
            if (claims.Count>0)
            {
                user = new User
                {
                    Id = Int32.Parse(claims.Find(c => c.Type == ClaimTypes.NameIdentifier).Value),
                    FirstName = claims.Find(c => c.Type == ClaimTypes.Name).Value,
                    LastName = claims.Find(c => c.Type == ClaimTypes.Surname).Value,
                    Email = claims.Find(c => c.Type == ClaimTypes.Email).Value,
                    Status = claims.Find(c => c.Type == CustomClaimTypes.Status)
                .Value == "true" ? true : false
                };
            }
            else
            {
                user = null;
            }
            _requestUserService.SetUser(user);

            await _next(httpContext);
        }

    }
}
