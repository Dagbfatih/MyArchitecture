using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class QuestionValidator : AbstractValidator<QuestionDetailsDto>
    {
        public QuestionValidator() // kurallar constructor içerisine yazılır. AbstactValidator ise FluentValidation'dan gelir.
        {
            RuleFor(q => q.QuestionText).MinimumLength(2).MaximumLength(800);
            RuleFor(q => q.Options).Must(MustMinTwoOption).WithMessage(Messages.MustBeMinTwoOption);
            RuleFor(q => q.Categories).Must(MustMinOneCategory).WithMessage(Messages.MustBeMinOneCategory);
            RuleFor(q => q.Options).Must(ChechIfOptionExistsOnQuestion).WithMessage(Messages.OptionExists);
            RuleFor(q => q.Categories).Must(ChechIfcategoryExistsOnQuestion).WithMessage(Messages.CategoryExists);
        }

        private bool ChechIfcategoryExistsOnQuestion(List<Category> arg)
        {
            var categoriesDuplicate = arg.GroupBy(c => c.CategoryId)
               .Any(g => g.Count() > 1);

            if (categoriesDuplicate)
            {
                return false;
            }
            return true;
        }

        private bool ChechIfOptionExistsOnQuestion(List<Option> arg)
        {
            var optionsDuplicate = arg.GroupBy(o => o.OptionText)
               .Any(g => g.Count() > 1);
            
            if (optionsDuplicate)
            {
                return false;
            }
            return true;
        }

        private bool MustMinOneCategory(List<Category> arg)
        {
            if (arg.Count < 1)
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
