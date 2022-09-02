using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ILessonService : IBusinessServiceRepository<Lesson>
    {
        IDataResult<List<Lesson>> GetAllByGradeLevel(int gradeLevelId);
    }
}
