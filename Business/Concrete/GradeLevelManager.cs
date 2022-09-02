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
    public class GradeLevelManager : BusinessMessagesService, IGradeLevelService
    {
        private readonly IGradeLevelDal _gradeLevelDal;

        public GradeLevelManager(IGradeLevelDal gradeLevelDal)
        {
            _gradeLevelDal = gradeLevelDal;
        }

        [SecuredOperation("admin")]
        public IResult Add(GradeLevel entity)
        {
            _gradeLevelDal.Add(entity);
            return new SuccessResult(_messages.GradeLevelAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(GradeLevel entity)
        {
            _gradeLevelDal.Delete(entity);
            return new SuccessResult(_messages.GradeLevelDeleted);
        }

        public IDataResult<GradeLevel> Get(int id)
        {
            return new SuccessDataResult<GradeLevel>(_gradeLevelDal.Get(g => g.Id == id));
        }

        public IDataResult<List<GradeLevel>> GetAll()
        {
            return new SuccessDataResult<List<GradeLevel>>(_gradeLevelDal.GetAll());
        }

        [SecuredOperation("admin")]
        public IResult Update(GradeLevel entity)
        {
            _gradeLevelDal.Update(entity);
            return new SuccessResult(_messages.GradeLevelUpdated);
        }
    }
}
