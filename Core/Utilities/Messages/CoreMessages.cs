using Core.Business.Contexts.TranslationContext;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Messages
{
    public class CoreMessages
    {
        ITranslationContext _translationContext;
        public CoreMessages()
        {
            _translationContext = ServiceTool.ServiceProvider.GetService<ITranslationContext>();
        }

        private string GetMessage(string key)
        {
            string result;
            if (_translationContext.Translates.TryGetValue(key, out result))
            {
                return result;
            }
            return "Internal Server Error";
        }

        public string InternalServerError { get { return GetMessage("internalServerError"); } }
        public string AuthorizationDenied { get { return GetMessage("authorizationDenied"); } }
        public string EmailSent { get { return GetMessage("emailSent"); } }

    }
}
