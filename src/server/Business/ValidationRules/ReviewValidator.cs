using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator()
        {
            RuleFor(p => p.Content).NotEmpty().Length(5, 500);
            RuleFor(p => p.Rating).NotEmpty().InclusiveBetween(1, 5);
        }
    }
}
