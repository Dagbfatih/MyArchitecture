using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProfileInfoManager : IProfileInfoService
    {

        private IProfileInfoDal _profileInfoDal;
        private ISchoolService _schoolService;

        public ProfileInfoManager(IProfileInfoDal profileInfoDal, ISchoolService schoolService)
        {
            _profileInfoDal = profileInfoDal;
            _schoolService = schoolService;
        }

        public IResult Add(ProfileInfo entity)
        {
            _profileInfoDal.Add(entity);
            return new SuccessResult();
        }

        public IResult UpdateInfos(ProfileInfoForUpdate profileInfo) // WİLL REFACTOR
        {
            UpdateSchool(profileInfo);

            var updatedProfileInfo = new ProfileInfo
            {
                About = profileInfo.About,
                DateOfBirth = profileInfo.DateOfBirth,
                LivingCityId = profileInfo.LivingCityId,
                SocialLinks = profileInfo.SocialLinks,
                UserId = profileInfo.UserId,
                GraduatedSchoolId = _schoolService.GetByUser(profileInfo.UserId).Data.Id,
                Id = profileInfo.ProfileInfoId
            };

            Update(updatedProfileInfo);
            return new SuccessResult();
        }

        private IResult UpdateSchool(ProfileInfoForUpdate profileInfo)
        {
            if (profileInfo.GraduatedSchool != null)
            {
                var result = _schoolService.GetByUser(profileInfo.UserId);
                if (result.Data == null) // if not exists
                {
                    var addedSchool = new School
                    {
                        Name = profileInfo.GraduatedSchool,
                        UserId = profileInfo.UserId
                    };
                    _schoolService.Add(addedSchool);
                }
                else // if exists
                {
                    var updatedSchool = result.Data;
                    updatedSchool.Name = profileInfo.GraduatedSchool;
                    _schoolService.Update(updatedSchool);
                }
            }

            return new SuccessResult();
        }

        public IResult Delete(ProfileInfo entity)
        {
            _profileInfoDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<ProfileInfo> Get(int id)
        {
            return new SuccessDataResult<ProfileInfo>(_profileInfoDal.Get(p => p.Id == id));
        }

        public IDataResult<List<ProfileInfo>> GetAll()
        {
            return new SuccessDataResult<List<ProfileInfo>>(_profileInfoDal.GetAll());
        }

        public IDataResult<List<ProfileInfoDetailsDto>> GetAllDetails()
        {
            return new SuccessDataResult<List<ProfileInfoDetailsDto>>(_profileInfoDal.GetAllDetails());
        }

        public IDataResult<ProfileInfoDetailsDto> GetDetailsByUser(int userId)
        {
            return new SuccessDataResult<ProfileInfoDetailsDto>(_profileInfoDal.GetDetailsByUser(userId));
        }

        public IResult Update(ProfileInfo entity)
        {
            _profileInfoDal.Update(entity);
            return new SuccessResult();
        }
    }
}
