using Core.Entities.Abstract;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Token : IEntity
    {
        public RefreshToken RefreshToken { get; set; }
        public AccessToken AccessToken { get; set; }
        
    }
}
