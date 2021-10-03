using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Core.Utilities.IoC;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Business.ValidationRules.FluentValidation
{
    public class QuestionValidator : AbstractValidator<QuestionDetailsDto>
    {
        private readonly Messages _messages;
        public QuestionValidator() // kurallar constructor içerisine yazılır. AbstactValidator ise FluentValidation'dan gelir.
        {
            _messages = ServiceTool.ServiceProvider.GetService<Messages>();

            RuleFor(q => q.Question.QuestionText).MinimumLength(2).MaximumLength(1000);
            RuleFor(q => q.Options).Must(MustMinTwoOption).WithMessage(_messages.MustBeMinTwoOption);
            RuleFor(q => q.Options).Must(CheckIfOptionExistsOnQuestion).WithMessage(_messages.OptionExists);
            RuleFor(q => q.Options).Must(CheckIfExistsCorrectOption).WithMessage(_messages.MustOneCorrectOption);
        }

        private bool CheckIfExistsCorrectOption(List<Option> arg)
        {
            return arg.Count(o => o.Accuracy) == 1;
        }

        private bool CheckIfOptionExistsOnQuestion(List<Option> arg)
        {
            var optionsDuplicate = arg.GroupBy(o => o.OptionText)
               .Any(g => g.Count() > 1);

            if (optionsDuplicate)
            {
                return false;
            }
            return true;
        }

        private bool MustMinTwoOption(List<Option> arg)
        {
            if (arg.Count < 2)
            {
                return false;
            }
            return true;
        }
    }
}
