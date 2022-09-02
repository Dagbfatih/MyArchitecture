using Business.Abstract;
using Business.Services;
using Core.Aspects.Autofac.Transaction;
using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Helpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RefreshTokenManager : BusinessMessagesService, IRefreshTokenService
    {
        private IRefreshTokenDal _refreshTokenDal;
        private IUserService _userService;
        private RefreshTokenHelper _refreshTokenHelper;

        public RefreshTokenManager(
            IRefreshTokenDal refreshTokenDal,
            RefreshTokenHelper refreshTokenHelper,
            IUserService userService)
        {
            _refreshTokenDal = refreshTokenDal;
            _refreshTokenHelper = refreshTokenHelper;
            _userService = userService;
        }

        public IResult Add(RefreshToken entity)
        {
            _refreshTokenDal.Add(entity);
            return new SuccessResult(_messages.RefreshTokenAdded);
        }

        public IResult CheckTokenExpire(string token)
        {
            var refreshToken = _refreshTokenDal.Get(r => r.Token == token);
            if (refreshToken == null)
            {
                return new ErrorResult();
            }

            if (refreshToken.ExpireTime <= DateTime.Now)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        public IResult Delete(RefreshToken entity)
        {
            _refreshTokenDal.Delete(entity);
            return new SuccessResult(_messages.RefreshTokenDeleted);
        }

        public IResult DeleteIfExists(User user)
        {
            var result = BusinessRules.Run(CheckIfTokenExists(user.Id));

            if (result != null)
            {
                var deletedToken = _refreshTokenDal.Get(r => r.UserId == user.Id);
                Delete(deletedToken);
            }

            return new SuccessResult();
        }

        public IDataResult<RefreshToken> GenerateRefreshToken(User user)
        {
            var refreshToken = _refreshTokenHelper.GenerateRefreshToken(user);
            return new SuccessDataResult<RefreshToken>(refreshToken);
        }

        public IDataResult<RefreshToken> Get(int id)
        {
            return new SuccessDataResult<RefreshToken>(_refreshTokenDal.Get(r => r.Id == id));
        }

        public IDataResult<List<RefreshToken>> GetAll()
        {
            return new SuccessDataResult<List<RefreshToken>>(_refreshTokenDal.GetAll());
        }

        public IDataResult<RefreshToken> GetByRefreshToken(string refreshToken)
        {
            var token = _refreshTokenDal.Get(r => r.Token == refreshToken);
            return new SuccessDataResult<RefreshToken>(token);
        }

        public IDataResult<RefreshToken> GetByUser(int userId)
        {
            return new SuccessDataResult<RefreshToken>(_refreshTokenDal.Get(r => r.UserId == userId));
        }

        public IResult Update(RefreshToken entity)
        {
            _refreshTokenDal.Update(entity);
            return new SuccessResult(_messages.RefreshTokenUpdated);
        }

        private IResult CheckIfTokenExists(int userId)
        {
            var refreshToken = _refreshTokenDal.Get(r => r.UserId == userId);
            if (refreshToken == null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
