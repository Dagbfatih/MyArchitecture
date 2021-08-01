using Business.Constants;
using Castle.DynamicProxy;
using Core.Business.Services;
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
        private bool _isUserProtection;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly Messages _messages;

        public SecuredOperation(string roles, bool isUserProtection = false)
        {
            _roles = roles.Split(',');
            _isUserProtection = isUserProtection;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _messages = ServiceTool.ServiceProvider.GetService<Messages>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            var existCustomAuthForUser = CheckCustomUserAuth(invocation);

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
            if (_isUserProtection)
            {
                int requestUserId = (int)invocation.Arguments.GetValue(0);
                var httpUserId = _httpContextAccessor.HttpContext?.User.Claims
                    .FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;

                if (httpUserId == null)
                {
                    return new ErrorResult(_messages.AuthorizationDenied);
                }

                if (requestUserId != Convert.ToInt32(httpUserId))
                {
                    return new ErrorResult(_messages.AuthorizationDenied);
                }
            }

            return new SuccessResult();
        }
    }
}
