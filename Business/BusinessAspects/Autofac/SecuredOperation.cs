using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using Business.Services;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly Messages _messages;
        private bool _isUserProtection;

        public SecuredOperation(string roles, bool isUserProtection = false)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _messages = ServiceTool.ServiceProvider.GetService<Messages>();
            _isUserProtection = isUserProtection;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

            if (roleClaims.Contains("admin"))
            {
                return;
            }

            var existCustomAuthForUser = _isUserProtection ?
                CheckCustomUserAuth(invocation) : new SuccessResult();

            if (!existCustomAuthForUser.Success)
            {
                throw new SecurityException(existCustomAuthForUser.Message);
            }

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role.Trim()))
                {
                    return;
                }
            }
            throw new SecurityException(_messages.AuthorizationDenied);
        }

        private IResult CheckCustomUserAuth(IInvocation invocation)
        {
            var requestUserValue = invocation.Arguments.GetValue(0);

            if (requestUserValue.GetType() == typeof(int))
            {
                var httpUserId = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                if ((int)requestUserValue != Convert.ToInt32(httpUserId) || httpUserId == null)
                {
                    return new ErrorResult(_messages.AuthorizationDenied);
                }
            }
            else if (requestUserValue.GetType() == typeof(string))
            {
                var httpUserMail = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type.EndsWith("email"))?.Value;

                if ((string)requestUserValue != httpUserMail || httpUserMail == null)
                {
                    return new ErrorResult(_messages.AuthorizationDenied);
                }
            }

            return new SuccessResult();
        }
    }
}
