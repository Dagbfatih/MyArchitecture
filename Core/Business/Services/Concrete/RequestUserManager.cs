using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Services.Concrete
{
    public class RequestUserManager : IRequestUserService
    {
        private User _user;

        public User GetUser()
        {
            return _user;
        }

        public void SetUser(User user)
        {
            _user = user;
        }
    }
}
