using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Core.Utilities.Errors
{
    public class ValidationErrorDetails : AbstractErrorDetails<ValidationException>
    {
        public List<string> ValidationErrors { get; set; }
        public ValidationErrorDetails(HttpContext httpContext, HttpStatusCode statusCode, IEnumerable<ValidationFailure> validationErrors) : base(statusCode)
        {
            ValidationErrors = validationErrors.ToList().ConvertAll<string>(v => v.ErrorMessage);
            httpContext.Response.StatusCode = (int)statusCode;
        }

        public ValidationErrorDetails(HttpContext httpContext, HttpStatusCode statusCode, string errorMessage, IEnumerable<ValidationFailure> validationErrors) : base(statusCode, errorMessage)
        {
            ValidationErrors = validationErrors.ToList().ConvertAll<string>(v => v.ErrorMessage);
            httpContext.Response.StatusCode = (int)statusCode;
        }
    }
}
