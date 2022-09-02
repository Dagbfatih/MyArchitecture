using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Core.Business.Contexts.TranslationContext;

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

        public string QuestionAdded { get { return GetMessage("questionAdded"); } }
        public string QuestionDeleted { get { return GetMessage("questionDeleted"); } }
        public string QuestionUpdated { get { return GetMessage("questionUpdated"); } }

        public string OptionAdded { get { return GetMessage("optionAdded"); } }
        public string OptionDeleted { get { return GetMessage("optionDeleted"); } }
        public string OptionUpdated { get { return GetMessage("optionUpdated"); } }

        public string CategoryAdded { get { return GetMessage("categoryAdded"); } }
        public string CategoryDeleted { get { return GetMessage("categoryDeleted"); } }
        public string CategoryUpdated { get { return GetMessage("categoryUpdated"); } }

        public string QuestionCategoryAdded { get { return GetMessage("questionCategoryAdded"); } }
        public string QuestionCategoryDeleted { get { return GetMessage("questionCategoryDeleted"); } }
        public string QuestionCategoryUpdated { get { return GetMessage("questionCategoryUpdated"); } }

        public string OptionsGot { get { return GetMessage("optionsGot"); } }
        public string OptionGot { get { return GetMessage("optionGot"); } }
        public string MustBeOnlyOneCorrectOption { get { return GetMessage("mustOnlyOneCorrectOption"); } }
        public string MustBeMinOneCategory { get { return GetMessage("mustMinOneCategory"); } }
        public string MustBeMinTwoOption { get { return GetMessage("mustMinTwoOption"); } }

        public string AuthorizationDenied { get { return GetMessage("authorizationDenied"); } }
        public string SuccessfulLogin { get { return GetMessage("successfulLogin"); } }
        public string UserRegistered { get { return GetMessage("userRegistered"); } }
        public string PasswordError { get { return GetMessage("passwordError"); } }
        public string UserNotFound { get { return GetMessage("userNotFound"); } }
        public string UserAlreadyExists { get { return GetMessage("userAlreadyExists"); } }
        public string AccessTokenCreated { get { return GetMessage("accessTokenCreated"); } }

        public string TestCreated { get { return GetMessage("testAdded"); } }
        public string TestDeleted { get { return GetMessage("testDeleted"); } }
        public string TestUpdated { get { return GetMessage("testUpdated"); } }

        public string DeleteFailed { get { return GetMessage("deleteFailed"); } }

        public string QuestionAddedToTest { get { return GetMessage("questionAddedToTest"); } }
        public string QuestionDeletedFromTest { get { return GetMessage("questionDeletedFromTest"); } }
        public string QuestionUpdatedForTest { get { return GetMessage("questionUpdatedForTest"); } }


        public string UserAdded { get { return GetMessage("userAdded"); } }
        public string UserDeleted { get { return GetMessage("userDeleted"); } }
        public string UserUpdated { get { return GetMessage("userUpdated"); } }

        public string QuestionTicketAdded { get { return GetMessage("questionTicketAdded"); } }
        public string QuestionTicketDeleted { get { return GetMessage("questionTicketDeleted"); } }
        public string QuestionTicketUpdated { get { return GetMessage("questionTicketUpdated"); } }

        public string TestTicketAdded { get { return GetMessage("testTicketAdded"); } }
        public string TestTicketDeleted { get { return GetMessage("testTicketDeleted"); } }
        public string TestTicketUpdated { get { return GetMessage("testTicketUpdated"); } }

        public string CustomerAdded { get { return GetMessage("customerAdded"); } }
        public string CustomerDeleted { get { return GetMessage("customerDeleted"); } }
        public string CustomerUpdated { get { return GetMessage("customerUpdated"); } }

        public string RoleAdded { get { return GetMessage("roleAdded"); } }
        public string RoleDeleted { get { return GetMessage("roleDeleted"); } }
        public string RoleUpdated { get { return GetMessage("roleUpdated"); } }

        public string CategoryExists { get { return GetMessage("categoryExists"); } }
        public string OptionExists { get { return GetMessage("optionExists"); } }
        public string QuestionExists { get { return GetMessage("questionExists"); } }
        public string EmailExists { get { return GetMessage("emailExists"); } }
        public string ProfileImagesLimited { get { return GetMessage("profileImagesLimited"); } }

        public string ProfileImageAdded { get { return GetMessage("profileImageAdded"); } }
        public string ProfileImageDeleted { get { return GetMessage("profileImageDeleted"); } }
        public string ProfileImageUpdated { get { return GetMessage("profileImageUpdated"); } }

        public string LanguageCreated { get { return GetMessage("languageCreated"); } }
        public string LanguageDeleted { get { return GetMessage("languageDeleted"); } }
        public string LanguageUpdated { get { return GetMessage("languageUpdated"); } }

        public string TranslateCreated { get { return GetMessage("translateAdded"); } }
        public string TranslateDeleted { get { return GetMessage("translateDeleted"); } }
        public string TranslateUpdated { get { return GetMessage("translateUpdated"); } }

        public string TestResultCreated { get { return GetMessage("testResultCreated"); } }
        public string TestResultDeleted { get { return GetMessage("testResultDeleted"); } }
        public string TestResultUpdated { get { return GetMessage("testResultUpdated"); } }

        public string QuestionResultCreated { get { return GetMessage("questionResultCreated"); } }
        public string QuestionResultDeleted { get { return GetMessage("questionResultDeleted"); } }
        public string QuestionResultUpdated { get { return GetMessage("questionResultUpdated"); } }

        public string OperationClaimAdded { get { return GetMessage("operationClaimAdded"); } }
        public string OperationClaimDeleted { get { return GetMessage("operationClaimDeleted"); } }
        public string OperationClaimUpdated { get { return GetMessage("operationClaimUpdated"); } }

        public string UserOperationClaimAdded { get { return GetMessage("userOperationClaimAdded"); } }
        public string UserOperationClaimDeleted { get { return GetMessage("userOperationClaimDeleted"); } }
        public string UserOperationClaimUpdated { get { return GetMessage("userOperationClaimUpdated"); } }

        public string AccountConfirmed { get { return GetMessage("accountConfirmed"); } }

        public string MustContainAtLeastNumerical { get { return GetMessage("mustContainAtLeastNumerical"); } }
        public string MustContainAtLeastUppercaseChar { get { return GetMessage("mustContainAtLeastUppercaseChar"); } }
        public string MustNotContainAtLeastSpace { get { return GetMessage("mustNotContainSpaces"); } }

        public string TranslateExists { get { return GetMessage("translateExists"); } }

        public string AllQuestionsMustContainSameNumberOption { get { return GetMessage("allQuestionsMustContainSameNumberOption"); } }

        public string MustOneCorrectOption { get { return GetMessage("mustOneCorrectOption"); } }

        public string BranchAdded { get { return GetMessage("branchAdded"); } }
        public string BranchDeleted { get { return GetMessage("branchDeleted"); } }
        public string BranchUpdated { get { return GetMessage("branchUpdated"); } }

        public string RoleIdNotNull { get { return GetMessage("roleIdNotNull"); } }

        public string DifficultyAdded { get { return GetMessage("added"); } }
        public string DifficultyDeleted { get { return GetMessage("added"); } }
        public string DifficultyUpdated { get { return GetMessage("added"); } }
        public string GradeLevelAdded { get { return GetMessage("added"); } }
        public string GradeLevelDeleted { get { return GetMessage("added"); } }
        public string GradeLevelUpdated { get { return GetMessage("added"); } }
        public string LessonCreated { get { return GetMessage("added"); } }
        public string LessonDeleted { get { return GetMessage("added"); } }
        public string LessonUpdated { get { return GetMessage("added"); } }
        public string SubjectAdded { get { return GetMessage("added"); } }
        public string SubjectDeleted { get { return GetMessage("added"); } }
        public string SubjectUpdated { get { return GetMessage("added"); } }
        public string RefreshTokenCreated { get { return GetMessage("added"); } }
        public string RefreshTokenAdded { get { return GetMessage("added"); } }
        public string RefreshTokenDeleted { get { return GetMessage("added"); } }
        public string RefreshTokenUpdated { get { return GetMessage("added"); } }
        public string RefreshTokenInvalid { get { return GetMessage("added"); } }
        public string RefreshTokenExpired { get { return GetMessage("added"); } }
    }
}
