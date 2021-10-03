using Business.Constants;
using Core.Utilities.IoC;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Business.ValidationRules.FluentValidation
{
    public class TestDetailsDtoValidator : AbstractValidator<TestDetailsDto>
    {
        private readonly Messages _messages;

        public TestDetailsDtoValidator()
        {
            _messages = ServiceTool.ServiceProvider.GetService<Messages>();

            RuleFor(t => t.Test.UserId).NotNull().NotEqual(0);
            RuleFor(t => t.Questions).Must(CheckIfQuestionExistsOnTest).WithMessage(_messages.QuestionExists);
            RuleFor(t => t.Test.Title).NotEmpty().NotNull();
            RuleFor(t => t.Questions).Must(CheckOptionNumbersIsDifferentForEach).WithMessage(_messages.AllQuestionsMustContainSameNumberOption);
        }

        private bool CheckOptionNumbersIsDifferentForEach(List<QuestionDetailsDto> arg)
        {
            return (arg.Select(q => q.Options.Count).Distinct().Count() <= 1);
        }

        private bool CheckIfQuestionExistsOnTest(List<QuestionDetailsDto> arg)
        {
            var questionDuplicate = arg.GroupBy(t => t.Question.QuestionId)
               .Any(g => g.Count() > 1);

            if (questionDuplicate)
            {
                return false;
            }
            return true;
        }
    }
}
