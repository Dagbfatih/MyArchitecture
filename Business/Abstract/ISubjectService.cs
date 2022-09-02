using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ISubjectService : IBusinessServiceRepository<Subject>
    {
        IDataResult<List<Subject>> GetAllByLesson(int lessonId);
        IDataResult<List<Subject>> GetALlByLessons(params int[] lessonIds);
    }
}
