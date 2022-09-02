using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<RefreshToken> CreateRefreshToken(User user);
        IDataResult<Token> CreateToken(User user);
        IDataResult<User> RegisterWithCustomer(UserForRegisterDto userForRegisterDto, string password, Customer customer);
        IResult RefreshTokenIsValid(string token);
        IResult RefreshTokenExpired(string token);
        IDataResult<RefreshToken> RefreshToken(User user);
    }
}
