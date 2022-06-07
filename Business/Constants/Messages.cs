using Core.Business.Contexts.TranslationContext;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Constants
{
    public class Messages
    {
        ITranslationContext _translationContext;

        public Messages()
        {
            _translationContext = ServiceTool.ServiceProvider.GetService<ITranslationContext>();
        }

        private string GetMessage(string key)
        {
            if (_translationContext.Translates.TryGetValue(key, out string result))
            {
                return result;
            }
            return "unknown";
        }

        
        public string AuthorizationDenied { get { return GetMessage("authorizationDenied"); } }
        public string SuccessfulLogin { get { return GetMessage("successfulLogin"); } }
        public string UserRegistered { get { return GetMessage("userRegistered"); } }
        public string PasswordError { get { return GetMessage("passwordError"); } }
        public string UserNotFound { get { return GetMessage("userNotFound"); } }
        public string UserAlreadyExists { get { return GetMessage("userAlreadyExists"); } }
        public string AccessTokenCreated { get { return GetMessage("accessTokenCreated"); } }

        public string UserAdded { get { return GetMessage("userAdded"); } }
        public string UserDeleted { get { return GetMessage("userDeleted"); } }
        public string UserUpdated { get { return GetMessage("userUpdated"); } }
        
        public string EmailExists { get { return GetMessage("emailExists"); } }
       
        public string LanguageCreated { get { return GetMessage("languageCreated"); } }
        public string LanguageDeleted { get { return GetMessage("languageDeleted"); } }
        public string LanguageUpdated { get { return GetMessage("languageUpdated"); } }

        public string TranslateCreated { get { return GetMessage("translateAdded"); } }
        public string TranslateDeleted { get { return GetMessage("translateDeleted"); } }
        public string TranslateUpdated { get { return GetMessage("translateUpdated"); } }

        public string OperationClaimAdded { get { return GetMessage("operationClaimAdded"); } }
        public string OperationClaimDeleted { get { return GetMessage("operationClaimDeleted"); } }
        public string OperationClaimUpdated { get { return GetMessage("operationClaimUpdated"); } }

        public string UserOperationClaimAdded { get { return GetMessage("userOperationClaimAdded"); } }
        public string UserOperationClaimDeleted { get { return GetMessage("userOperationClaimDeleted"); } }
        public string UserOperationClaimUpdated { get { return GetMessage("userOperationClaimUpdated"); } }

        public string MustContainAtLeastNumerical { get { return GetMessage("mustContainAtLeastNumerical"); } }
        public string MustContainAtLeastUppercaseChar { get { return GetMessage("mustContainAtLeastUppercaseChar"); } }
        public string MustNotContainAtLeastSpace { get { return GetMessage("mustNotContainSpaces"); } }

        public string TranslateExists { get { return GetMessage("translateExists"); } }
        
        public string RefreshTokenCreated { get { return GetMessage("added"); } }
        public string RefreshTokenAdded { get { return GetMessage("added"); } }
        public string RefreshTokenDeleted { get { return GetMessage("added"); } }
        public string RefreshTokenUpdated { get { return GetMessage("added"); } }
        public string RefreshTokenInvalid { get { return GetMessage("added"); } }
        public string RefreshTokenExpired { get { return GetMessage("added"); } }
    }
}
