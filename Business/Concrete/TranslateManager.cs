using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Dtos;
using System.Linq;
using Business.Services;
using Core.Business;
using Business.BusinessAspects.Autofac;

namespace Business.Concrete
{
    public class TranslateManager :BusinessMessagesService,  ITranslateService
    {
        ITranslateDal _translateDal;
        public TranslateManager(ITranslateDal translateDal)
        {
            _translateDal = translateDal;
        }

        [CacheRemoveAspect("ITranslateService.Get")]
        [SecuredOperation("admin")]
        public IResult Add(Translate entity)
        {
            var result = BusinessRules.Run(CheckIfExists(entity));
            if (result!=null)
            {
                return result;
            }

            _translateDal.Add(entity);
            return new SuccessResult(_messages.TranslateCreated);
        }

        private IResult CheckIfExists(Translate entity)
        {
            var translates = GetAllByLanguage(entity.LanguageId);
            foreach (var translate in translates.Data)
            {
                if (translate.Key==entity.Key)
                {
                    return new ErrorResult(_messages.TranslateExists);
                }
            }

            return new SuccessResult();
        }

        [CacheRemoveAspect("ITranslateService.Get")]
        [SecuredOperation("admin")]
        public IResult Delete(Translate entity)
        {
            _translateDal.Delete(entity);
            return new SuccessResult(_messages.TranslateDeleted);
        }

        [CacheAspect]
        public IDataResult<Translate> Get(int id)
        {
            return new SuccessDataResult<Translate>(_translateDal.Get(t => t.Id == id));
        }

        [CacheAspect]
        public IDataResult<List<Translate>> GetAll()
        {
            return new SuccessDataResult<List<Translate>>(_translateDal.GetAll());
        }

        public IDataResult<List<Translate>> GetAllByCode(string code)
        {
            return new SuccessDataResult<List<Translate>>(_translateDal
                .GetAllByCode(code));
        }

        public IDataResult<Dictionary<string, string>> GetAllByCodeAsDictionary(string code)
        {
            var d = _translateDal.GetAllByCode(code).Select(t=>t.Key);
            var result = _translateDal.GetAllByCode(code)
                .ToDictionary(t => t.Key, t => t.Value);
            return new SuccessDataResult<Dictionary<string, string>>(result);
        }

        public IDataResult<List<Translate>> GetAllByLanguage(int languageId)
        {
            return new SuccessDataResult<List<Translate>>(_translateDal
                .GetAll(t => t.LanguageId == languageId));
        }

        public IDataResult<Dictionary<string, string>> GetAllByLanguageAsDictionary(int languageId)
        {
            var result = _translateDal.GetAll(t=>t.LanguageId==languageId)
                .ToDictionary(t => t.Key, t => t.Value);
            return new SuccessDataResult<Dictionary<string, string>>(result);
        }

        public IDataResult<List<TranslateDetailsDto>> GetAllDetails()
        {
            return new SuccessDataResult<List<TranslateDetailsDto>>(_translateDal
                .GetAllDetails());
        }

        public IDataResult<List<TranslateDetailsDto>> GetAllDetailsByCode(string code)
        {
            return new SuccessDataResult<List<TranslateDetailsDto>>(_translateDal
                .GetAllDetailsByCode(code));
        }

        public IDataResult<List<TranslateDetailsDto>> GetAllDetailsByLanguage(int languageId)
        {
            return new SuccessDataResult<List<TranslateDetailsDto>>(_translateDal
                .GetAllDetailsByLanguageId(languageId));
        }

        public IDataResult<string> GetByKeyAsString(string key, int languageId)
        {
            return new SuccessDataResult<string>(_translateDal
                .Get(t => t.Key == key && t.LanguageId == languageId).Value);
        }

        public IDataResult<string> GetByKeyAsString(string key, string code)
        {
            var result = _translateDal
                .GetByKeyAndCode(key, code);

            if (result == null)
            {
                return new ErrorDataResult<string>("default");
            }

            return new SuccessDataResult<string>(result.Value);
        }

        [CacheRemoveAspect("ITranslateService.Get")]
        [SecuredOperation("admin")]
        public IResult Update(Translate entity)
        {
            _translateDal.Update(entity);
            return new SuccessResult(_messages.TranslateUpdated);
        }
    }
}
