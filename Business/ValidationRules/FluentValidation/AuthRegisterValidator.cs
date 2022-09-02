using Business.Constants;
using Core.Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthRegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        private readonly Messages _messages;

        public AuthRegisterValidator()
        {
            _messages = ServiceTool.ServiceProvider.GetService<Messages>();

            RuleFor(u => u.Password).MaximumLength(16).MinimumLength(8);
            RuleFor(u => u.Password).NotEmpty().NotNull();
            RuleFor(u => u.Password).Must(MustContainAtLeastUppercase).
                WithMessage(_messages.MustContainAtLeastUppercaseChar); // en az bir büyük harf
            //RuleFor(u => u.Password).Must(MustContainAtLeastNumerical).
            //    WithMessage(_messages.MustContainAtLeastNumerical); // en az bir rakam
            RuleFor(u => u.Password).Must(MustNotContainAtLeastSpace).
                WithMessage(_messages.MustNotContainAtLeastSpace); // boşluk içermemeli
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

        private bool MustContainAtLeastNumerical(string arg)
        {
            var ifContainAtLeastNumerical = false;
            foreach (var character in arg)
            {
                if (Char.IsNumber(character))
                {
                    ifContainAtLeastNumerical = true;
                }
            }

            return ifContainAtLeastNumerical;
        }

        private bool MustContainAtLeastUppercase(string arg)
        {
            var ifContainsAtLeastUppercase = false;
            foreach (var character in arg)
            {
                if (Char.IsUpper(character))
                {
                    ifContainsAtLeastUppercase = true;
                }
            }

            return ifContainsAtLeastUppercase;
        }
    }
}
