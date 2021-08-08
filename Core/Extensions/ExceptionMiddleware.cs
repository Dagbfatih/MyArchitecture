using Core.Utilities.IoC;
using Core.Utilities.Messages;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.Errors;
using System.Linq;
using Core.Utilities.Services.Translate;

namespace Core.Extensions
{
    public class ExceptionMiddleware:CoreMessagesService
	{
		private RequestDelegate _next;
		private IErrorDetails _errorDetails;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
			_errorDetails = ServiceTool.ServiceProvider.GetService<IErrorDetails>();
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception e)
			{
				await HandleExceptionAsync(httpContext, e);
			}
		}

		private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
		{
			httpContext.Response.ContentType = "application/json";
			httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			_ = e.Message;
			
			if (e.GetType() == typeof(ValidationException))
			{
				var validationErrors = ((ValidationException)e).Errors;
				_errorDetails = new ValidationErrorDetails(httpContext, HttpStatusCode.BadRequest, validationErrors);
			}
			else if (e.GetType() == typeof(ApplicationException))
			{
				_errorDetails = new DefaultErrorDetails(HttpStatusCode.BadRequest, e.Message);
			}
			else if (e.GetType() == typeof(UnauthorizedAccessException))
			{
				_errorDetails = new DefaultErrorDetails(HttpStatusCode.Unauthorized, e.Message);
			}
			else if (e.GetType() == typeof(SecurityException))
			{
				_errorDetails = new DefaultErrorDetails(HttpStatusCode.Unauthorized, e.Message);
            }
			else
			{
				_errorDetails = new DefaultErrorDetails(HttpStatusCode.InternalServerError,
					_coreMessages.InternalServerError);
			}

            await httpContext.Response.WriteAsync(_errorDetails.ToString());
		}
	}
}
