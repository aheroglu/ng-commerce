using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(5, 150);
            RuleFor(p => p.Brand).NotEmpty().Length(1, 30);
            RuleFor(p => p.Model).NotEmpty().Length(1, 50);
            RuleFor(p => p.UrlHandle).NotEmpty();
            RuleFor(p => p.ImageUrl).NotEmpty();
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
            RuleFor(p => p.StockQuantity).NotEmpty().GreaterThan(0);
        }
    }
}
