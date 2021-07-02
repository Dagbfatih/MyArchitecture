using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.UserId).NotEqual(0).NotNull();
            RuleFor(c => c.RoleId).NotEqual(0).NotNull();
        }
    }
}
