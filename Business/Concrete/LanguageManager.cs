using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class LanguageManager : ILanguageService
    {
        ILanguageDal _languageDal;
        public LanguageManager(ILanguageDal languageDal)
        {
            _languageDal = languageDal;
        }

        public IResult Add(Language entity)
        {
            _languageDal.Add(entity);
            return new SuccessResult(Messages.LanguageCreated);
        }

        public IResult Delete(Language entity)
        {
            _languageDal.Delete(entity);
            return new SuccessResult(Messages.LanguageDeleted);
        }

        public IDataResult<Language> Get(int id)
        {
            return new SuccessDataResult<Language>(_languageDal.Get(l => l.Id == id));
        }

        public IDataResult<List<Language>> GetAll()
        {
            return new SuccessDataResult<List<Language>>(_languageDal.GetAll());
        }

        public IResult Update(Language entity)
        {
            _languageDal.Update(entity);
            return new SuccessResult(Messages.LanguageUpdated);
        }
    }
}
