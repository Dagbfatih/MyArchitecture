using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Business.Services;

namespace Business.Concrete
{
    public class LanguageManager : BusinessMessagesService, ILanguageService
    {
        ILanguageDal _languageDal;
        public LanguageManager(ILanguageDal languageDal)
        {
            _languageDal = languageDal;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ILanguageService.Get")]
        public IResult Add(Language entity)
        {
            _languageDal.Add(entity);
            return new SuccessResult(_messages.LanguageCreated);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ILanguageService.Get")]
        public IResult Delete(Language entity)
        {
            _languageDal.Delete(entity);
            return new SuccessResult(_messages.LanguageDeleted);
        }

        [CacheAspect]
        public IDataResult<Language> Get(int id)
        {
            return new SuccessDataResult<Language>(_languageDal.Get(l => l.Id == id));
        }

        //[CacheAspect]
        public IDataResult<List<Language>> GetAll()
        {
            return new SuccessDataResult<List<Language>>(_languageDal.GetAll());
        }

        public IDataResult<Language> GetLanguageByCode(string code)
        {
            return new SuccessDataResult<Language>(_languageDal.Get(l => l.Code == code));
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ILanguageService.Get")]
        public IResult Update(Language entity)
        {
            _languageDal.Update(entity);
            return new SuccessResult(_messages.LanguageUpdated);
        }

    
    }
}
