using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Services;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class DifficultyManager : BusinessMessagesService, IDifficultyService
    {
        IDifficultyDal _difficultyDal;
        public DifficultyManager(IDifficultyDal difficultyDal)
        {
            _difficultyDal = difficultyDal;
        }

        [SecuredOperation("admin")]
        public IResult Add(Difficulty entity)
        {
            _difficultyDal.Add(entity);
            return new SuccessResult(_messages.DifficultyAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Difficulty entity)
        {
            _difficultyDal.Delete(entity);
            return new SuccessResult(_messages.DifficultyDeleted);
        }

        public IDataResult<Difficulty> Get(int id)
        {
            return new SuccessDataResult<Difficulty>(_difficultyDal.Get(d => d.Id == id));
        }

        public IDataResult<List<Difficulty>> GetAll()
        {
            return new SuccessDataResult<List<Difficulty>>(_difficultyDal.GetAll());

        }

        [SecuredOperation("admin")]
        public IResult Update(Difficulty entity)
        {
            _difficultyDal.Update(entity);
            return new SuccessResult(_messages.DifficultyUpdated);
        }
    }
}
