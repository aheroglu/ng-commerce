using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class NewsletterValidator : AbstractValidator<Newsletter>
    {
        public NewsletterValidator()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
        }
    }
}
