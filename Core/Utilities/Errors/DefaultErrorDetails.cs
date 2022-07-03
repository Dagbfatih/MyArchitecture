using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Utilities.Errors
{
    public class DefaultErrorDetails : AbstractErrorDetails<Exception>
    {
        public DefaultErrorDetails(HttpStatusCode statusCode, string errorMessage)
            : base(statusCode, errorMessage)
        {

        }

        public DefaultErrorDetails(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(statusCode)
        {

        }
    }
}
