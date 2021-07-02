using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Utilities.Errors
{
    public class AbstractErrorDetails<TException> : IErrorDetails
    {
        public string ExceptionType { get { return typeof(TException).Name; } }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public AbstractErrorDetails(HttpStatusCode statusCode, string errorMessage)
        {
            this.StatusCode = statusCode;
            this.ErrorMessage = errorMessage;
        }

        public AbstractErrorDetails(HttpStatusCode statusCode) : this(statusCode, default)
        {
            this.StatusCode = statusCode;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
