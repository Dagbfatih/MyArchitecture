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

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private bool _isUserProtection;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles, bool isUserProtection = false)
        {
            _roles = roles.Split(',');
            _isUserProtection = isUserProtection;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
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
            throw new SecurityException(Messages.AuthorizationDenied);
        }

        private IResult CheckCustomUserAuth(IInvocation invocation)
        {
            if (_isUserProtection)
            {
                if (_httpContextAccessor.HttpContext.User.Claims.ToList().Count == 0)
                {
                    return new ErrorResult(Messages.AuthorizationDenied);
                }

                int requestUserId = (int)invocation.Arguments.GetValue(0);
                int httpUserId = Int32.Parse(_httpContextAccessor.HttpContext.User.
                    FindFirst(ClaimTypes.NameIdentifier).Value);

                if (requestUserId != httpUserId)
                {
                    return new ErrorResult(Messages.AuthorizationDenied);
                }
            }

            return new SuccessResult();
        }
    }
}
