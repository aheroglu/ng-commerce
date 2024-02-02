using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.AddressTitle).NotEmpty();
            RuleFor(p => p.ZipCode).NotEmpty();
            RuleFor(p => p.Total).NotEmpty();
            RuleFor(p => p.CityId).NotEmpty();
            RuleFor(p => p.DistrictId).NotEmpty();
        }
    }
}
