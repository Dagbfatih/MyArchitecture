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
    public class SubjectManager : BusinessMessagesService, ISubjectService
    {
        private readonly ISubjectDal _subjectDal;

        public SubjectManager(ISubjectDal subjectDal)
        {
            _subjectDal = subjectDal;
        }

        [SecuredOperation("admin")]
        public IResult Add(Subject entity)
        {
            _subjectDal.Add(entity);
            return new SuccessResult(_messages.SubjectAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Subject entity)
        {
            _subjectDal.Delete(entity);
            return new SuccessResult(_messages.SubjectDeleted);
        }

        public IDataResult<Subject> Get(int id)
        {
            return new SuccessDataResult<Subject>(_subjectDal.Get(s => s.Id == id));
        }

        public IDataResult<List<Subject>> GetAll()
        {
            return new SuccessDataResult<List<Subject>>(_subjectDal.GetAll());
        }

        public IDataResult<List<Subject>> GetAllByLesson(int lessonId)
        {
            return new SuccessDataResult<List<Subject>>(_subjectDal.GetAll(s => s.LessonId == lessonId));
        }

        public IDataResult<List<Subject>> GetALlByLessons(params int[] lessonIds)
        {
            List<Subject> result = new List<Subject>();

            foreach (var lessonId in lessonIds)
            {
                var subjects = GetAllByLesson(lessonId).Data;
                if (subjects != null)
                {
                    result.AddRange(subjects);
                }
            }
            return new SuccessDataResult<List<Subject>>(result);
        }

        [SecuredOperation("admin")]
        public IResult Update(Subject entity)
        {
            _subjectDal.Update(entity);
            return new SuccessResult(_messages.SubjectUpdated);
        }
    }
}
