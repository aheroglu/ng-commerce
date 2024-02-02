using Entity.DTOs.ProfileDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class UserInformationValidator : AbstractValidator<UpdateUserInformationDTO>
    {
        public UserInformationValidator()
        {
            RuleFor(p => p.UserName).NotEmpty();
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.PhoneNumber).NotEmpty();
        }
    }
}
