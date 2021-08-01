using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Core.Business.Contexts.TranslationContext;
using WebAPI.Extensions.Models;

namespace WebAPI.Extensions.Middlewares
{
    public class TranslateMiddleware
    {
        private RequestDelegate _next;
        private ITranslateService _translateService;
        private ITranslationContext _translationContext;

        public TranslateMiddleware(RequestDelegate next,
            ITranslateService translateService)
        {
            _next = next;
            _translateService = translateService;
            _translationContext = ServiceTool.ServiceProvider
                .GetService<ITranslationContext>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            StringValues languageCode;

            if (httpContext.Request.Headers.TryGetValue("language", out languageCode))
            {
                _translationContext.Translates = _translateService
                    .GetAllByCodeAsDictionary(languageCode).Data;
            }
            else
            {
                _translationContext.Translates = _translateService
                    .GetAllByCodeAsDictionary(LanguageCodes.English).Data;
            }

            await _next(httpContext);
        }
    }
}
