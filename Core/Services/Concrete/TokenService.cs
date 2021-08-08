using Core.Entities.Concrete;
using Core.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private User _user;
        private List<string> _claims;

        public TokenService()
        {
            _user = new User();
            _claims = new List<string>();
        }

        public User GetUser()
        {
            return _user;
        }

        public List<string> GetClaims()
        {
            return _claims;
        }

        public void SetUser(User user)
        {
            _user = user;
        }

        public void SetClaims(List<string> claims)
        {
            _claims = claims;
        }
    }
}
