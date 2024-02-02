using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly INewsletterService _newsletterService;

        public DashboardController(IProductService productService, UserManager<AppUser> userManager, IOrderService orderService, INewsletterService newsletterService)
        {
            _productService = productService;
            _userManager = userManager;
            _orderService = orderService;
            _newsletterService = newsletterService;
        }

        [HttpGet("CountOfProducts")]
        public async Task<IActionResult> CountOfProducts()
        {
            var values = await _productService.CountOfProducts();
            return Ok(values);
        }

        [HttpGet("CountOfMembers")]
        public async Task<IActionResult> CountOfMembers()
        {
            var values = await _userManager.GetUsersInRoleAsync("Member");
            return Ok(values.Count);
        }

        [HttpGet("CountOfOrders")]
        public async Task<IActionResult> CountOfOrders()
        {
            var values = await _orderService.CountOfOrders();
            return Ok(values);
        }

        [HttpGet("CountOfSubscribers")]
        public async Task<IActionResult> CountOfSubscribers()
        {
            var values = await _newsletterService.CountOfSubscribers();
            return Ok(values);
        }

        [HttpGet("LastFiveOrder")]
        public async Task<IActionResult> LastFiveOrder()
        {
            var values = await _orderService.LastFiveOrder();
            return Ok(values);
        }

        [HttpGet("TopSellerProducts")]
        public async Task<IActionResult> TopSellerProducts()
        {
            var values = await _productService.TopSellerProducts();
            return Ok(values);
        }

        [HttpGet("TopSellerProductsForHome")]
        public async Task<IActionResult> TopSellerProductsForHome()
        {
            var values = await _productService.TopSellerProductsForHome();
            return Ok(values);
        }
    }
}
