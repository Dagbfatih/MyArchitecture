using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Extensions.Middlewares
{
    public static class TranslateMiddlewareExtensions
    {
        public static void ConfigureCustomTranslateMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TranslateMiddleware>();
        }
    }
}
