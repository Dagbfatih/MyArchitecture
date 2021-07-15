using Business.Abstract;
using Business.Constants;
using Core.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProfileImageManager : IProfileImageService
    {
        IProfileImageDal _profileImageDal;

        public ProfileImageManager(IProfileImageDal profileImageDal)
        {
            _profileImageDal = profileImageDal;
        }

        public IResult Add(ProfileImage entity, IFormFile formFile)
        {
            var result = BusinessRules.Run(CheckImageLimited(entity.UserId));
            if (result != null)
            {
                return result;
            }

            entity.ImagePath = FileHelper.Add(formFile);
            entity.Date = DateTime.Now;
            _profileImageDal.Add(entity);
            return new SuccessResult(Messages.ProfileImageAdded);
        }

        private IResult CheckImageLimited(int userId)
        {
            int profileImageCount = _profileImageDal.GetAll(i => i.UserId == userId).Count;
            if (profileImageCount > 0)
            {
                return new ErrorResult();
            }
            return new SuccessResult(Messages.ProfileImagesLimited);
        }

        public IResult Delete(ProfileImage entity)
        {
            var oldpath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) +
                 _profileImageDal.Get(i => i.Id == entity.Id).ImagePath;
            var result = BusinessRules.Run(FileHelper.Delete(oldpath));
            
            if (result != null)
            {
                return result;
            }

            _profileImageDal.Delete(entity);
            return new SuccessResult(Messages.ProfileImageDeleted);
        }

        public IDataResult<ProfileImage> Get(int id)
        {
            return new SuccessDataResult<ProfileImage>(_profileImageDal.Get(i => i.Id == id));
        }

        public IDataResult<List<ProfileImage>> GetAll()
        {
            return new SuccessDataResult<List<ProfileImage>>(_profileImageDal.GetAll());
        }

        public IDataResult<ProfileImage> GetImageByUserId(int userId)
        {
            var result = _profileImageDal.Get(i => i.UserId == userId);
            if (result == null)
            {
                ProfileImage profileImage = new ProfileImage
                {
                    UserId = userId,
                    ImagePath = @"\Images\defaultProfileImage.png"
                };

                return new SuccessDataResult<ProfileImage>(profileImage);
            }
            return new SuccessDataResult<ProfileImage>(_profileImageDal.Get(i => i.UserId == userId));
        }

        public IResult Update(ProfileImage entity, IFormFile formFile)
        {
            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) +
                _profileImageDal.Get(cI => cI.Id == entity.Id).ImagePath;

            entity.ImagePath = FileHelper.Update(oldPath, formFile);
            entity.Date = DateTime.Now;
            _profileImageDal.Update(entity);
            return new SuccessResult(Messages.ProfileImageUpdated);
        }
    }
}
