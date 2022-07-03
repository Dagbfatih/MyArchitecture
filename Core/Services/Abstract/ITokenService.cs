using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Abstract
{
    public interface ITokenService
    {
        User GetUser();
        List<string> GetClaims();
        void SetUser(User user);
        void SetClaims(List<string> claims);
    }
}
