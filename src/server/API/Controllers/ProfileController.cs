using AutoMapper;
using Business.ValidationRules;
using Entity.Concrete;
using Entity.DTOs.AppUserDTOs;
using Entity.DTOs.ProfileDTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ProfileController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("GetUserInformation/{email}")]
        public async Task<IActionResult> GetUserInformation(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return NotFound();

            var result = _mapper.Map<AppUserDTO>(user);

            return Ok(result);
        }

        [HttpPut("UpdateUserInformation")]
        public async Task<IActionResult> EditUserInformation(UpdateUserInformationDTO model)
        {
            UserInformationValidator validator = new UserInformationValidator();
            ValidationResult result = await validator.ValidateAsync(model);

            if (result.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is null) return NotFound();

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Gender = model.Gender;
                user.PhoneNumber = model.PhoneNumber;

                var updateResult = await _userManager.UpdateAsync(user);

                var userResult = _mapper.Map<AppUserDTO>(user);

                if (updateResult.Succeeded) return Ok(userResult);
            }

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("UpdateUserPassword")]
        public async Task<IActionResult> UpdateUserPassword(UpdatePasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) return NotFound();

            bool isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);

            if (isCurrentPasswordValid)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);

                await _userManager.UpdateAsync(user);

                return Ok();
            }

            else return BadRequest("Current password is wrong!");
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteAccount(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok("Account deleted successfully");
            }

            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}