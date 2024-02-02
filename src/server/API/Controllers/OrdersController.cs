using AutoMapper;
using Business.Abstract;
using Business.MailServices;
using Business.ValidationRules;
using DataAccess.Concrete.Contexts;
using Entity.Concrete;
using Entity.DTOs.OrderDTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly OrderCompletedMailService _orderCompletedMailService;

        public OrdersController(IOrderService orderService, IOrderItemService orderItemService, UserManager<AppUser> userManager, IMapper mapper, AppDbContext context, IProductService productService, OrderCompletedMailService orderCompletedMailService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _productService = productService;
            _orderCompletedMailService = orderCompletedMailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersWithRelations();

            if (orders.Count is 0) return NoContent();

            var values = _mapper.Map<List<OrderDTO>>(orders);

            return Ok(values);
        }

        [HttpGet("GetOrdersByUser/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == userId);

            if (user is null) return NotFound("User Not Found!");

            var orders = await _orderService.GetOrdersByUser(userId);

            if (orders.Count is 0) return NoContent();

            var values = _mapper.Map<List<OrderDTO>>(orders);

            return Ok(values);
        }

        [HttpGet("GetOrderDetailsByOrder/{orderNo}")]
        public async Task<IActionResult> GetOrderDetailsByOrder(int orderNo)
        {
            var orderItems = await _orderService.GetOrderDetailsByOrder(orderNo);

            if (orderItems is null) return NotFound();

            var values = _mapper.Map<OrderDTO>(orderItems);

            return Ok(values);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderModel)
        {
            var random = new Random();
            var orderNo = random.Next(1000000, 2000000);

            if (_context.Orders.Any(p => p.OrderNo == orderNo))
            {
                orderNo = random.Next(1000000, 2000000);
            }

            var order = new Order
            {
                OrderNo = orderNo,
                AddressTitle = orderModel.AddressTitle,
                Address = orderModel.Address,
                ZipCode = orderModel.ZipCode,
                Total = orderModel.Total,
                CityId = orderModel.CityId,
                DistrictId = orderModel.DistrictId,
                AppUserId = orderModel.AppUserId,
                CreatedAt = DateTime.Now
            };

            OrderValidator validator = new OrderValidator();
            ValidationResult result = await validator.ValidateAsync(order);

            if (result.IsValid)
            {
                await _orderService.Insert(order);
                var user = await _userManager.FindByEmailAsync(orderModel.Email);
                _orderCompletedMailService.SendMail(user.UserName, user.Email, orderNo);
            }

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }

            foreach (var cartItem in orderModel.CartItems)
            {
                var orderItem = new OrderItem
                {
                    Quantity = cartItem.Quantity,
                    ProductId = cartItem.ProductId,
                    CreatedAt = DateTime.Now,
                    OrderId = order.Id
                };

                await _orderItemService.Insert(orderItem);
                await _productService.IncreaseStock(cartItem.ProductId, cartItem.Quantity);
            }


            return Ok(order.Id);
        }
    }
}
