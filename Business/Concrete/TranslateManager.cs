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
    public class TranslateManager : ITranslateService
    {
        ITranslateDal _translateDal;
        public TranslateManager(ITranslateDal translateDal)
        {
            _translateDal = translateDal;
        }

        public IResult Add(Translate entity)
        {
            _translateDal.Add(entity);
            return new SuccessResult(Messages.TranslateCreated);
        }

        public IResult Delete(Translate entity)
        {
            _translateDal.Delete(entity);
            return new SuccessResult(Messages.TranslateDeleted);
        }

        public IDataResult<Translate> Get(int id)
        {
            return new SuccessDataResult<Translate>(_translateDal.Get(t => t.Id == id));
        }

        public IDataResult<List<Translate>> GetAll()
        {
            return new SuccessDataResult<List<Translate>>(_translateDal.GetAll());
        }

        public IResult Update(Translate entity)
        {
            _translateDal.Update(entity);
            return new SuccessResult(Messages.TranslateUpdated);
        }
    }
}
