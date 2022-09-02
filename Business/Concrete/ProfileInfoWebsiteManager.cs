using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProfileInfoWebsiteManager : IProfileInfoWebsiteService
    {

        private IProfileInfoWebsiteDal _profileInfoWebsiteDal;

        public ProfileInfoWebsiteManager(IProfileInfoWebsiteDal profileInfoWebsiteDal)
        {
            _profileInfoWebsiteDal = profileInfoWebsiteDal;
        }

        public IResult Add(ProfileInfoWebsite entity)
        {
            _profileInfoWebsiteDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(ProfileInfoWebsite entity)
        {
            _profileInfoWebsiteDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<ProfileInfoWebsite> Get(int id)
        {
            return new SuccessDataResult<ProfileInfoWebsite>(_profileInfoWebsiteDal.Get(p => p.Id == id));
        }

        public IDataResult<List<ProfileInfoWebsite>> GetAll()
        {
            return new SuccessDataResult<List<ProfileInfoWebsite>>(_profileInfoWebsiteDal.GetAll());
        }

        public IResult Update(ProfileInfoWebsite entity)
        {
            _profileInfoWebsiteDal.Update(entity);
            return new SuccessResult();
        }
    }
}
