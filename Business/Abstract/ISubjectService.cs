using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ISubjectService : IBusinessService<Subject>
    {
        IDataResult<List<Subject>> GetAllByLesson(int lessonId);
    }
}
