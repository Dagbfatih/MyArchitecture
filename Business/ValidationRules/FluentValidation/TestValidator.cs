using Business.Constants;
using Core.Utilities.IoC;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Business.ValidationRules.FluentValidation
{
    public class TestValidator : AbstractValidator<TestDetailsDto>
    {
        private readonly Messages _messages;
        public TestValidator()
        {
            _messages = ServiceTool.ServiceProvider.GetService<Messages>();

            RuleFor(t => t.Test.UserId).NotNull().NotEqual(0);
            RuleFor(t => t.Test.Title).NotEmpty().NotNull();
            RuleFor(t => t.Test.TestTime).NotEqual(0).GreaterThan(0);
        }
    }
}
