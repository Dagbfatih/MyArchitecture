using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Services
{
    public interface IRequestUserService
    {
        User GetUser();
        void SetUser(User user);
    }
}
