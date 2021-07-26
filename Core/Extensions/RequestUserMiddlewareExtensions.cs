using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class RequestUserMiddlewareExtensions
    {
        public static void ConfigureCustomRequestUserMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestUserMiddleware>();
        }
    }
}
