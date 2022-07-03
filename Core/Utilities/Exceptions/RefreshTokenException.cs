using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Utilities.Exceptions
{
    public class RefreshTokenException : Exception
    {
        public RefreshTokenException(HttpStatusCode httpStatusCode)
        {
            StatusCode = httpStatusCode;
        }

        public HttpStatusCode StatusCode { get; set; }

    }
}
