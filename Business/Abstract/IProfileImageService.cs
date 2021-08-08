using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProfileImageService
    {
        IResult Add(ProfileImage entity, IFormFile formFile);
        IResult Delete(ProfileImage entity);
        IResult Update(ProfileImage entity, IFormFile formFile);
        IDataResult<List<ProfileImage>> GetAll();
        IDataResult<ProfileImage> Get(int id);
        IDataResult<ProfileImage> GetImageByUserId(int userId);
        IDataResult<List<ProfileImage>> GetImagesByUsers(params int[] userId);
    }
}
