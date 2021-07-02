using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class QuestionTicketValidator : AbstractValidator<QuestionTicket>
    {
        public QuestionTicketValidator()
        {
            RuleFor(q => q.QuestionId).NotEqual(0).NotNull();
            RuleFor(t => t.UserId).NotEqual(0).NotNull();
        }
    }
}
