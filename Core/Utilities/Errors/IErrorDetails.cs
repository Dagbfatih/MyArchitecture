using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Utilities.Errors
{
    public interface IErrorDetails
    {
        string ExceptionType { get; }
        HttpStatusCode StatusCode { get; set; }
        string ErrorMessage { get; set; }
    }
}
