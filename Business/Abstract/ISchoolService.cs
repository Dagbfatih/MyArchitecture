using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISchoolService : IBusinessServiceRepository<School>
    {
        IDataResult<School> GetByUser(int userId);
    }
}
