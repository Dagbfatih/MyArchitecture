using Business.Concrete;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Business.Constants;
using Business.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.ValidationRules.FluentValidation
{
    public class OptionValidator : AbstractValidator<Option>
    {
        IOptionService _optionManager;
        public OptionValidator()
        {
            _optionManager = new OptionManager(new EfOptionDal());
            //RuleFor(o => o.QuestionId).Must(CannotBeMoreThanOneCorrectOption).When(o => o.CorrectOption).WithMessage(Messages.MustBeOnlyOneCorrectOption);
            RuleFor(o => o.OptionText).NotEmpty();
        }

        private bool CannotBeMoreThanOneCorrectOption(int arg)
        {
            var questionsOptions = _optionManager.GetAllByQuestionId(arg);
            var result = questionsOptions.Data.Any(o => o.Accuracy);
            return !result;
        }
    }
}
