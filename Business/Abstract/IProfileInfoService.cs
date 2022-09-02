using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProfileInfoService : IBusinessServiceRepository<ProfileInfo>
    {
        IResult UpdateInfos(ProfileInfoForUpdate profileInfo);
        IDataResult<List<ProfileInfoDetailsDto>> GetAllDetails();
        IDataResult<ProfileInfoDetailsDto> GetDetailsByUser(int userId);
    }
}
