using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
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
    public class ProfileImageManager : BusinessMessagesService, IProfileImageService
    {
        IProfileImageDal _profileImageDal;

        public ProfileImageManager(IProfileImageDal profileImageDal)
        {
            _profileImageDal = profileImageDal;
        }

        [SecuredOperation("user")]
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
            return new SuccessResult(_messages.ProfileImageAdded);
        }

        private IResult CheckImageLimited(int userId)
        {
            int profileImageCount = _profileImageDal.GetAll(i => i.UserId == userId).Count;
            if (profileImageCount > 0)
            {
                return new ErrorResult();
            }
            return new SuccessResult(_messages.ProfileImagesLimited);
        }

        [SecuredOperation("user")]
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
            return new SuccessResult(_messages.ProfileImageDeleted);
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
            if (result.ImagePath == null)
            {
                ProfileImage profileImage = new ProfileImage
                {
                    UserId = userId,
                    ImagePath = @"\Images\defaultProfileImage.png",
                    Date = result.Date
                };

                return new SuccessDataResult<ProfileImage>(profileImage);
            }
            return new SuccessDataResult<ProfileImage>(result);
        }

        [SecuredOperation("user")]
        public IResult Update(ProfileImage entity, IFormFile formFile)
        {
            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) +
                _profileImageDal.Get(cI => cI.Id == entity.Id).ImagePath;

            entity.ImagePath = FileHelper.Update(oldPath, formFile);
            entity.Date = DateTime.Now;
            _profileImageDal.Update(entity);
            return new SuccessResult(_messages.ProfileImageUpdated);
        }

        public IDataResult<List<ProfileImage>> GetImagesByUsers(params int[] userIds)
        {
            List<ProfileImage> result = new List<ProfileImage>();

            foreach (var userId in userIds)
            {
                var profileImage = _profileImageDal.Get(p => p.UserId == userId);
                if (profileImage == null)
                {
                    profileImage = new ProfileImage
                    {
                        UserId = userId,
                        ImagePath = @"\Images\defaultProfileImage.png"
                    };
                }

                result.Add(profileImage);
            }
            return new SuccessDataResult<List<ProfileImage>>(result);
        }
    }
}
