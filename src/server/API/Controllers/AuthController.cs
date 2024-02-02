using Business.MailServices;
using DataAccess.Concrete.Contexts;
using Entity.Concrete;
using Entity.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AccountCreatedMailService _accountCreatedMailService;
        private readonly ForgotPasswordMailService _forgotPasswordMailService;
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IConfiguration configuration, AccountCreatedMailService accountCreatedMailService, AppDbContext appDbContext, ForgotPasswordMailService forgotPasswordMailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _accountCreatedMailService = accountCreatedMailService;
            _appDbContext = appDbContext;
            _forgotPasswordMailService = forgotPasswordMailService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender
                };

                bool isAccountCreated = await _appDbContext.Users.AnyAsync(p => p.Email == model.Email);

                if (isAccountCreated)
                {
                    return BadRequest("This email address already used another account!");
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");
                    _accountCreatedMailService.SendMail(model.UserName, model.Email);
                    return Ok(user);
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

                return BadRequest(ModelState);
            }

            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is null) return NotFound("User Not Found!");

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

                if (result.IsLockedOut) return BadRequest(result.IsLockedOut.ToString());

                if (result.Succeeded)
                    return Ok(new
                    {
                        token = await GenerateJwtToken(user)
                    });

                else return BadRequest("User Not Found!");
            }

            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is null) return NotFound("Email is not registered!");

                _forgotPasswordMailService.SendMail(model.Email);

                return Ok();
            }

            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is null) return NotFound("Email is not registered!");

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

                await _userManager.UpdateAsync(user);

                return Ok();
            }

            else
            {
                return BadRequest(ModelState);
            }
        }

        private async Task<string> GenerateJwtToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
