using AutoMapper;
using Business.Abstract;
using Business.MailServices;
using Business.ValidationRules;
using Entity.Concrete;
using Entity.DTOs.NewsletterDTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewslettersController : ControllerBase
    {
        private readonly INewsletterService _newsletterService;
        private readonly IMapper _mapper;
        private readonly NewsletterMailService _newsletterMailService;
        private readonly CancelNewsletterSubscriptionMailService _cancelNewsletterSubscription;
        private readonly UserManager<AppUser> _userManager;

        public NewslettersController(IMapper mapper, INewsletterService newsletterService, NewsletterMailService newsletterMailService, UserManager<AppUser> userManager, CancelNewsletterSubscriptionMailService cancelNewsletterSubscription)
        {
            _mapper = mapper;
            _newsletterService = newsletterService;
            _newsletterMailService = newsletterMailService;
            _userManager = userManager;
            _cancelNewsletterSubscription = cancelNewsletterSubscription;
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsletters()
        {
            var newsletters = await _newsletterService.GetAll();

            if (newsletters.Count is 0) return NoContent();

            var values = _mapper.Map<List<NewsletterDTO>>(newsletters);

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsletter(int id)
        {
            var newsletter = await _newsletterService.GetById(id);

            if (newsletter is null) return NotFound();

            var values = _mapper.Map<NewsletterDTO>(newsletter);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewsletter(AddNewsletterDTO model)
        {
            var newsletter = _mapper.Map<Newsletter>(model);

            NewsletterValidator validator = new NewsletterValidator();
            ValidationResult result = await validator.ValidateAsync(newsletter);

            if (result.IsValid)
            {
                bool isSubscribed = await _newsletterService.IsSubscribed(model.Email);

                if (isSubscribed)
                {
                    return BadRequest("This email already subscribed!");
                }

                await _newsletterService.Insert(newsletter);
                var user = await _userManager.FindByEmailAsync(model.Email);
                _newsletterMailService.SendMail(user.UserName, user.Email);
                return Ok(newsletter);
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

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteNewsletter(string email)
        {
            var newsletter = await _newsletterService.GetNewsletterByEmail(email);

            if (newsletter is null) return NotFound();

            await _newsletterService.Delete(newsletter);

            var user = await _userManager.FindByEmailAsync(email);

            _cancelNewsletterSubscription.SendMail(user.UserName, email);

            return Ok(newsletter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNewsletter(int id, UpdateNewsletterDTO model)
        {
            var newsletter = await _newsletterService.GetById(id);

            if (newsletter is null) return NotFound();

            newsletter.Email = model.Email;

            NewsletterValidator validator = new NewsletterValidator();
            ValidationResult result = await validator.ValidateAsync(newsletter);

            if (result.IsValid)
            {
                await _newsletterService.Update(newsletter);
                return Ok(newsletter);
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

        [HttpGet("CheckSubscription/{email}")]
        public async Task<IActionResult> CheckSubcription(string email)
        {
            bool isSubscribed = await _newsletterService.IsSubscribed(email);
            return Ok(isSubscribed);
        }
    }
}