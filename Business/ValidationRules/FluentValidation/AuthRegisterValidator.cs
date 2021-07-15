using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthRegisterValidator:AbstractValidator<UserForRegisterDto>
    {
        public AuthRegisterValidator()
        {
            RuleFor(u => u.Password).MaximumLength(16).MinimumLength(8);
            RuleFor(u => u.Password).NotEmpty().NotNull();
        }
    }
}
