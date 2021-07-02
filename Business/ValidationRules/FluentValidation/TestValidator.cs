using Business.Constants;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class TestValidator : AbstractValidator<TestDetailsDto>
    {
        public TestValidator()
        {
            RuleFor(t => t.UserId).NotNull().NotEqual(0);
            RuleFor(t => t.Questions).Must(CheckIfQuestionExistsOnTest).WithMessage(Messages.QuestionExists);
            RuleFor(t => t.TestName).NotEmpty().NotNull();
        }

        private bool CheckIfQuestionExistsOnTest(List<QuestionDetailsDto> arg)
        {
            var questionDuplicate = arg.GroupBy(t => t.QuestionId)
               .Any(g => g.Count() > 1);

            if (questionDuplicate)
            {
                return false;
            }
            return true;
        }
    }
}
