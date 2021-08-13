using Business.Constants;
using Core.Utilities.IoC;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        private readonly Messages _messages;

        public CustomerValidator()
        {
            _messages = ServiceTool.ServiceProvider.GetService<Messages>();
            RuleFor(c => c.UserId).NotEqual(0).NotNull();
            RuleFor(c => c.RoleId).NotEqual(0).NotNull().WithMessage(_messages.RoleIdNotNull);
        }
    }
}
