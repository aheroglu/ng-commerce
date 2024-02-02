using Entity.Concrete;
using FluentValidation;

namespace Business.ValidationRules
{
    public class MailValidator : AbstractValidator<Mail>
    {
        public MailValidator()
        {
            RuleFor(p => p.For).NotEmpty();
            RuleFor(p => p.Subject).NotEmpty();
            RuleFor(p => p.Content).NotEmpty();
        }
    }
}
