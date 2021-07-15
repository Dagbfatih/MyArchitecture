using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Messages
{
    public static class ExceptionMessage
    {
        public static string InternalServerError => "Something went wrong. Please try again.";

        public static string AuthorizationDenied => "Yetki Reddedildi";
    }
}
