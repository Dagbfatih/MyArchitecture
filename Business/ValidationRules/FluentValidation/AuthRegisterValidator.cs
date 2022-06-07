using Business.Constants;
using Core.Entities.Dtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthRegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        private readonly Messages _messages;

        public AuthRegisterValidator()
        {
            _messages = ServiceTool.ServiceProvider.GetService<Messages>();

            RuleFor(u => u.Password).NotEmpty().NotNull();
            RuleFor(u => u.Password).Must(MustNotContainAtLeastSpace).
                WithMessage(_messages.MustNotContainAtLeastSpace);
        }

        private bool MustNotContainAtLeastSpace(string arg)
        {
            var ifNotContainAtLeastSpace = true;
            foreach (var character in arg)
            {
                if (Char.IsWhiteSpace(character))
                {
                    ifNotContainAtLeastSpace = false;
                }
            }

            return ifNotContainAtLeastSpace;
        }
    }
}
