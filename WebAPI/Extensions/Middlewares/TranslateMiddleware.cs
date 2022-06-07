using Business.Abstract;
using Core.Business.Contexts.TranslationContext;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;
using WebAPI.Extensions.Constants;

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
