using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserWithTokenDto:IDto
    {
        public User User { get; set; }
        public AccessToken Token { get; set; }
    }
}
