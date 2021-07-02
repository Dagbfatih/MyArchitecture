using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class TestTicketValidator:AbstractValidator<TestTicket>
    {
        public TestTicketValidator()
        {
            RuleFor(t => t.TestId).NotEqual(0).NotNull();
            RuleFor(t => t.UserId).NotEqual(0).NotNull();
        }
    }
}
