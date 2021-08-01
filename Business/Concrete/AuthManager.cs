using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Entities.Concrete;
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

        public AuthManager(IUserService userService,
            ITokenHelper tokenHelper,
            ICustomerService customerService,
            IUserOperationClaimService userOperationClaimService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _customerService = customerService;
            _userOperationClaimService = userOperationClaimService;
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
            var userId = _userService.AddWithId(user);
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
            List<UserOperationClaim> claims = new List<UserOperationClaim>()
            {
                new UserOperationClaim
                {
                    OperationClaimId=2,
                    UserId=customer.Customer.UserId
                }
            };

            if (customer.Customer.RoleId == 1)
            {
                claims.Add(new UserOperationClaim
                {
                    OperationClaimId = 4,
                    UserId = customer.Customer.UserId
                });
            }
            else
            {
                claims.Add(new UserOperationClaim
                {
                    OperationClaimId = 2002,
                    UserId = customer.Customer.UserId
                });
            }

            _userOperationClaimService.AddClaims(claims);
        }
    }
}
