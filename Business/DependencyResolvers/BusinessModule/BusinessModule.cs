using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Core.Business.Contexts.TranslationContext;
using Core.Utilities.IoC;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.BusinessModule
{
    public class BusinessModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ITranslateService, TranslateManager>();
            serviceCollection.AddSingleton<ITranslateDal, EfTranslateDal>();
            serviceCollection.AddSingleton<Messages>();
            //serviceCollection.AddSingleton<IConfiguration>();

        }
    }
}
