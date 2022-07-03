using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class RefreshTokenHelper
    {
        public RefreshToken GenerateRefreshToken(User user)
        {
            RefreshToken refreshToken = new RefreshToken()
            {
                Token = GenerateRefreshToken(),
                UserId = user.Id,
                ExpireTime = DateTime.Now.AddHours(10).AddMinutes(10)
            };

            return refreshToken;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
