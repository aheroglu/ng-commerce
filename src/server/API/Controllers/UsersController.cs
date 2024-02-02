using AutoMapper;
using Business.MailServices;
using Entity.Concrete;
using Entity.DTOs.AppUserDTOs;
using Entity.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly AccountDeletedMailService _accountDeletedMailService;
        private readonly AdminAccountCreatedMailService _adminAccountCreatedMailService;

        public UsersController(UserManager<AppUser> userManager, IMapper mapper, AccountDeletedMailService accountDeletedMailService, AdminAccountCreatedMailService adminAccountCreatedMailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _accountDeletedMailService = accountDeletedMailService;
            _adminAccountCreatedMailService = adminAccountCreatedMailService;
        }

        [HttpGet("GetAllMembers")]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _userManager.GetUsersInRoleAsync("Member");

            if (members.Count is 0) return NoContent();

            var values = _mapper.Map<List<AppUserDTO>>(members);

            return Ok(values);
        }

        [HttpGet("GetAllAdmins/{currentAdminId}")]
        public async Task<IActionResult> GetAllAdmins(string currentAdminId)
        {
            var currentAdmin = await _userManager.FindByIdAsync(currentAdminId);

            if (currentAdmin is null) return NotFound();

            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            if (admins.Count is 0) return NoContent();

            admins = admins.Where(p => p.Id != currentAdmin.Id).ToList();

            var values = _mapper.Map<List<AppUserDTO>>(admins);

            return Ok(values);
        }

        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin(AddAdminDTO model)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                _adminAccountCreatedMailService.SendMail(model.UserName, model.Email, model.Password);
                return Ok("Account has been created successfully");
            }

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }
    }
}