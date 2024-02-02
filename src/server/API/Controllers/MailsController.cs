using AutoMapper;
using Business.Abstract;
using Business.MailServices;
using Business.ValidationRules;
using Entity.Concrete;
using Entity.DTOs.MailDTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly INewsletterService _newsletterService;
        private readonly IMapper _mapper;
        private readonly SendForSubscribers _sendForSubscribers;
        private readonly SendForAdmins _sendForAdmins;
        private readonly SendForMembers _sendForMembers;
        private readonly UserManager<AppUser> _userManager;

        public MailsController(IMailService mailService, IMapper mapper, SendForSubscribers sendForSubscribers, INewsletterService newsletterService, UserManager<AppUser> userManager, SendForMembers sendForMembers, SendForAdmins sendForAdmins)
        {
            _mailService = mailService;
            _mapper = mapper;
            _sendForSubscribers = sendForSubscribers;
            _newsletterService = newsletterService;
            _userManager = userManager;
            _sendForMembers = sendForMembers;
            _sendForAdmins = sendForAdmins;
        }

        [HttpGet]
        public async Task<IActionResult> GetMails()
        {
            var mails = await _mailService.GetAll();

            if (mails.Count is 0) return NotFound();

            var values = _mapper.Map<List<MailDTO>>(mails);

            return Ok(values);
        }

        [HttpGet("MailsByMembership/{membership}")]
        public async Task<IActionResult> MailsByMembership(string membership)
        {
            var mails = await _mailService.MailsByMembership(membership);

            if (mails.Count is 0) return NotFound();

            var values = _mapper.Map<List<MailDTO>>(mails);

            return Ok(values);
        }

        [HttpPost("SendForSubscribers")]
        public async Task<IActionResult> SendForSubscribers(AddMailDTO model)
        {
            var mail = _mapper.Map<Mail>(model);

            MailValidator validator = new MailValidator();
            ValidationResult result = await validator.ValidateAsync(mail);

            if (result.IsValid)
            {
                await _mailService.Insert(mail);

                var subscribers = await _newsletterService.GetAll();

                foreach (var subscriber in subscribers)
                {
                    _sendForSubscribers.SendMail(subscriber.Email, model.Subject, model.Content);
                }

                var values = _mapper.Map<MailDTO>(mail);

                return Ok(values);
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

        [HttpPost("SendForMembers")]
        public async Task<IActionResult> SendForMembers(AddMailDTO model)
        {
            var mail = _mapper.Map<Mail>(model);

            MailValidator validator = new MailValidator();
            ValidationResult result = await validator.ValidateAsync(mail);

            if (result.IsValid)
            {
                await _mailService.Insert(mail);

                var members = await _userManager.GetUsersInRoleAsync("Member");

                foreach (var member in members)
                {
                    _sendForMembers.SendMail(member.UserName, member.Email, model.Subject, model.Content);
                }

                var values = _mapper.Map<MailDTO>(mail);

                return Ok(values);
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

        [HttpPost("SendForAdmins")]
        public async Task<IActionResult> SendForAdmins(AddMailDTO model)
        {
            var mail = _mapper.Map<Mail>(model);

            MailValidator validator = new MailValidator();
            ValidationResult result = await validator.ValidateAsync(mail);

            if (result.IsValid)
            {
                await _mailService.Insert(mail);

                var admins = await _userManager.GetUsersInRoleAsync("Admin");

                foreach (var admin in admins)
                {
                    _sendForAdmins.SendMail(admin.UserName, admin.Email, model.Subject, model.Content);
                }

                var values = _mapper.Map<MailDTO>(mail);

                return Ok(values);
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
    }
}
