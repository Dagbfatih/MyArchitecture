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
    public class LessonManager : BusinessMessagesService, ILessonService
    {
        private readonly ILessonDal _lessonDal;

        public LessonManager(ILessonDal lessonDal)
        {
            _lessonDal = lessonDal;
        }

        [SecuredOperation("admin")]
        public IResult Add(Lesson entity)
        {
            _lessonDal.Add(entity);
            return new SuccessResult(_messages.LessonCreated);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Lesson entity)
        {
            _lessonDal.Delete(entity);
            return new SuccessResult(_messages.LessonDeleted);
        }

        public IDataResult<Lesson> Get(int id)
        {
            return new SuccessDataResult<Lesson>(_lessonDal.Get(l => l.Id == id));
        }

        public IDataResult<List<Lesson>> GetAll()
        {
            return new SuccessDataResult<List<Lesson>>(_lessonDal.GetAll());
        }

        public IDataResult<List<Lesson>> GetAllByGradeLevel(int gradeLevelId)
        {
            return new SuccessDataResult<List<Lesson>>(_lessonDal.GetAll(l => l.GradeLevelId == gradeLevelId));
        }

        [SecuredOperation("admin")]
        public IResult Update(Lesson entity)
        {
            _lessonDal.Update(entity);
            return new SuccessResult(_messages.LessonUpdated);
        }
    }
}
