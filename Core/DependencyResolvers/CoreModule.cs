using Core.Business.Contexts.TranslationContext;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Extensions;
using Core.Services.Abstract;
using Core.Services.Concrete;
using Core.Utilities.Errors;
using Core.Utilities.Helpers;
using Core.Utilities.IoC;
using Core.Utilities.Mail;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
            serviceCollection.AddSingleton<IErrorDetails, DefaultErrorDetails>();
            serviceCollection.AddSingleton<ITokenService, TokenService>();
            serviceCollection.AddSingleton<RefreshTokenHelper>();
            serviceCollection.AddSingleton<FileHelper>();
            serviceCollection.AddSingleton<CoreMessages>();
            serviceCollection.AddSingleton<JwtSecurityTokenHandler>();
            serviceCollection.AddSingleton<ITranslationContext, TranslationContext>();
            serviceCollection.AddSingleton<IEmailConfiguration, EmailConfiguration>();
            serviceCollection.AddSingleton<IEmailService, EmailManager>();
        }
    }
}
