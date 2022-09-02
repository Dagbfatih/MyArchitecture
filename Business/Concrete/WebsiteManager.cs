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
    public class WebsiteManager : IWebsiteService
    {

        private IWebsiteDal _websiteDal;

        public WebsiteManager(IWebsiteDal websiteDal)
        {
            _websiteDal = websiteDal;
        }

        public IResult Add(Website entity)
        {
            _websiteDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Website entity)
        {
            _websiteDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<Website> Get(int id)
        {
            return new SuccessDataResult<Website>(_websiteDal.Get(w => w.Id == id));
        }

        public IDataResult<List<Website>> GetAll()
        {
            return new SuccessDataResult<List<Website>>(_websiteDal.GetAll());
        }

        public IResult Update(Website entity)
        {
            _websiteDal.Update(entity);
            return new SuccessResult();
        }
    }
}
