using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
{
    public static class TokenMiddlewareExtensions
	{
		public static void ConfigureCustomTokenMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<TokenMiddleware>();
		}
	}
}
