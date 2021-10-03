using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : BusinessMessagesService, IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        [SecuredOperation("admin")]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetByMail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);
            return new SuccessDataResult<User>(result);
        }

        public IResult Add(User entity)
        {
            _userDal.Add(entity);
            return new SuccessResult(_messages.UserAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessResult(_messages.UserDeleted);
        }

        public IResult Update(User entity)
        {
            _userDal.Update(entity);
            return new SuccessResult(_messages.UserUpdated);
        }

        public IDataResult<User> Get(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            result.PasswordHash = null;
            result.PasswordSalt = null;

            return new SuccessDataResult<User>(result);
        }

        public IResult UpdateWithoutPassword(User user)
        {
            var updatedUser = new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Status = user.Status
            };

            _userDal.Update(user, u => u.FirstName, u => u.LastName, u => u.Email, u => u.Status);
            return new SuccessResult(_messages.UserUpdated);
        }

        public int AddWithId(User user)
        {
            return _userDal.Add(user).Id;
        }

        public IDataResult<List<User>> GetAllByIds(params int[] userIds)
        {
            List<User> result = new List<User>();

            foreach (var userId in userIds)
            {
                var user = Get(userId).Data;
                if (user != null)
                {
                    result.Add(user);
                }
            }
            return new SuccessDataResult<List<User>>(result);
        }
    }
}
