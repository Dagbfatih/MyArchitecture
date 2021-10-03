using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Helpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Business.Concrete
{
    public class AuthManager : BusinessMessagesService, IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private ICustomerService _customerService;
        private IUserOperationClaimService _userOperationClaimService;
        private IOperationClaimService _operationClaimService;
        private IRefreshTokenService _refreshTokenService;

        public AuthManager(
            IUserService userService,
            ITokenHelper tokenHelper,
            ICustomerService customerService,
            IUserOperationClaimService userOperationClaimService,
            IOperationClaimService operationClaimService,
            IRefreshTokenService refreshTokenService
            )
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _customerService = customerService;
            _userOperationClaimService = userOperationClaimService;
            _operationClaimService = operationClaimService;
            _refreshTokenService = refreshTokenService;
        }

        [ValidationAspect(typeof(AuthRegisterValidator))]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var result = BusinessRules.Run(UserExists(userForRegisterDto.Email));
            if (result != null)
            {
                return new ErrorDataResult<User>(result.Message);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, _messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(null, _messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(_messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, _messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            var result = _userService.GetByMail(email);
            if (result.Data != null)
            {
                return new ErrorResult(_messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, _messages.AccessTokenCreated);
        }

        [TransactionScopeAspect]
        [ValidationAspect(typeof(AuthRegisterValidator))]
        public IDataResult<User> RegisterWithCustomer(UserForRegisterDto userForRegisterDto, string password, Customer customer)
        {
            var result = BusinessRules.Run(UserExists(userForRegisterDto.Email));
            if (result != null)
            {
                return new ErrorDataResult<User>(result.Message);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            int userId = _userService.AddWithId(user);
            user.Id = userId;

            customer.UserId = userId;
            var customerAddResult = _customerService.Add(customer);
            if (!customerAddResult.Success)
            {
                throw new TransactionException(customerAddResult.Message);
            }

            AddClaims(new CustomerForRegisterDto
            {
                Customer = customer,
                User = userForRegisterDto
            });

            return new SuccessDataResult<User>(user, _messages.UserRegistered);
        }

        private void AddClaims(CustomerForRegisterDto customer)
        {
            var existClaims = _operationClaimService.GetAll().Data;

            List<UserOperationClaim> claims = new List<UserOperationClaim>()
            {
                new UserOperationClaim
                {
                    OperationClaimId = existClaims.FirstOrDefault(c => c.Name == "user").Id,
                    UserId = customer.Customer.UserId
                }
            };

            if (customer.Customer.RoleId == 1)
            {
                claims.Add(new UserOperationClaim
                {
                    OperationClaimId = existClaims.FirstOrDefault(c => c.Name == "student").Id,
                    UserId = customer.Customer.UserId
                });
            }
            else
            {
                claims.Add(new UserOperationClaim
                {
                    OperationClaimId = existClaims.FirstOrDefault(c => c.Name == "notConfirmedInstructor").Id,
                    UserId = customer.Customer.UserId
                });
            }

            _userOperationClaimService.AddClaims(claims);
        }

        public IDataResult<RefreshToken> CreateRefreshToken(User user)
        {
            _refreshTokenService.DeleteIfExists(user);
            var refreshToken = _refreshTokenService.GenerateRefreshToken(user).Data;
            _refreshTokenService.Add(refreshToken);

            return new SuccessDataResult<RefreshToken>(refreshToken, _messages.RefreshTokenCreated);
        }

        [TransactionScopeAspect]
        public IDataResult<Token> CreateToken(User user)
        {
            var token = new Token
            {
                AccessToken = CreateAccessToken(user).Data,
                RefreshToken = CreateRefreshToken(user).Data
            };

            return new SuccessDataResult<Token>(token);
        }

        public IResult RefreshTokenIsValid(string token)
        {
            var refreshToken = _refreshTokenService.GetByRefreshToken(token).Data;

            if (refreshToken == null)
            {
                return new ErrorResult(_messages.RefreshTokenInvalid);
            }

            return new SuccessResult();
        }

        public IResult RefreshTokenExpired(string token)
        {
            var refreshToken = _refreshTokenService.GetByRefreshToken(token).Data;

            if (refreshToken != null)
            {
                if (refreshToken.ExpireTime < DateTime.Now)
                {
                    return new ErrorResult(_messages.RefreshTokenExpired);
                }
            }
            return new SuccessResult();
        }

        public IDataResult<RefreshToken> RefreshToken(User user)
        {
            var refreshToken = _refreshTokenService.GetByUser(user.Id).Data;
            if (refreshToken != null)
            {
                refreshToken = CreateRefreshToken(user).Data;
            }

            return new SuccessDataResult<RefreshToken>(refreshToken);
        }
    }
}
