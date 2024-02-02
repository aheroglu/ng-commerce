using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(2, 30);
            RuleFor(p => p.UrlHandle).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
        }
    }
}
