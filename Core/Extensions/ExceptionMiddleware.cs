using Core.Utilities.Errors;
using Core.Utilities.IoC;
using Core.Utilities.Services.Translate;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Security;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware : CoreMessagesService
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
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else if (e.GetType() == typeof(ApplicationException))
            {
                _errorDetails = new DefaultErrorDetails(HttpStatusCode.BadRequest, e.Message);
            }
            else if (e.GetType() == typeof(UnauthorizedAccessException))
            {
                _errorDetails = new DefaultErrorDetails(HttpStatusCode.Unauthorized, e.Message);
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(SecurityException))
            {
                _errorDetails = new DefaultErrorDetails(HttpStatusCode.Unauthorized, e.Message);
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType()== typeof(SecurityTokenExpiredException))
            {
                _errorDetails = new DefaultErrorDetails(HttpStatusCode.Unauthorized, e.Message);
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                _errorDetails = new DefaultErrorDetails(HttpStatusCode.InternalServerError,
                    _coreMessages.InternalServerError);
            }
            httpContext.Response.StatusCode = (int)_errorDetails.StatusCode;

            await httpContext.Response.WriteAsync(_errorDetails.ToString());
        }
    }
}
