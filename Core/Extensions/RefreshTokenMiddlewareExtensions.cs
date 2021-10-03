using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
	public static class RefreshTokenMiddlewareExtensions
	{
		public static void ConfigureCustomRefreshTokenMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<RefreshTokenMiddleware>();
		}
	}
}
