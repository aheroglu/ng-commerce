using AutoMapper;
using Business.Abstract;
using Business.ValidationRules;
using Entity.Concrete;
using Entity.DTOs.ProductDTOs;
using Entity.DTOs.ProductImageDTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(IProductService productService, IProductImageService productImageService, IMapper mapper, ICategoryService categoryService, IWebHostEnvironment hostEnvironment)
        {
            _productService = productService;
            _productImageService = productImageService;
            _mapper = mapper;
            _categoryService = categoryService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsWithRelation();

            if (products.Count == 0) return NoContent();

            var values = _mapper.Map<List<ProductDTO>>(products);

            return Ok(values);
        }

        [HttpGet("{urlHandle}")]
        public async Task<IActionResult> GetProduct(string urlHandle)
        {
            var product = await _productService.GetProductWithRelation(urlHandle);

            if (product is null) return NotFound();

            var values = _mapper.Map<ProductDTO>(product);

            return Ok(values);
        }

        [HttpGet("GetProductByCategory/{urlHandle}")]
        public async Task<IActionResult> GetProductByCategory(string urlHandle)
        {
            var category = await _categoryService.GetByUrlHandle(urlHandle);

            if (category is null) return NotFound(urlHandle + " Not Found!");

            var products = await _productService.GetProductsByCategory(urlHandle);

            if (products.Count == 0) return NoContent();

            var values = _mapper.Map<List<ProductDTO>>(products);

            return Ok(values);
        }

        [HttpGet("GetRelatedProductsByCategory/{urlHandle}")]
        public async Task<IActionResult> GetRelatedProductsByCategory(string urlHandle)
        {
            var category = await _categoryService.GetByUrlHandle(urlHandle);

            if (category is null) return NotFound(urlHandle + " Not Found!");

            var products = await _productService.GetRelatedProductsByCategory(urlHandle);

            if (products.Count == 0) return NoContent();

            var values = _mapper.Map<List<ProductDTO>>(products);

            return Ok(values);
        }

        [HttpGet("SortProductsByPrice/{sortOrder}/{categoryId?}")]
        public async Task<IActionResult> SortProductsByPrice(string sortOrder, int? categoryId)
        {
            if (sortOrder is null) return BadRequest("Sort parameter must be enter!");

            List<Product> products = new();

            if (sortOrder is "LowToHigh")
            {
                if (categoryId is not null)
                {
                    products = await _productService.SortProductsLowToHigh(categoryId);
                }

                else
                {
                    products = await _productService.SortProductsLowToHigh();
                }
            }
            else if (sortOrder is "HighToLow")
            {
                if (categoryId is not null)
                {
                    products = await _productService.SortProductsHighToLow(categoryId);
                }

                else
                {
                    products = await _productService.SortProductsHighToLow();
                }
            }
            else
            {
                return BadRequest("Invalid sort parameter!");
            }

            if (products.Count is 0)
            {
                return NoContent();
            }

            var values = _mapper.Map<List<ProductDTO>>(products);

            return Ok(values);

        }

        [HttpGet("PopularMobilePhones")]
        public async Task<IActionResult> PopularMobilePhones()
        {
            var popularMobilePhones = await _productService.PopularMobilePhones();

            if (popularMobilePhones.Count == 0) return NoContent();

            var values = _mapper.Map<List<ProductDTO>>(popularMobilePhones);

            return Ok(values);
        }

        [HttpGet("PopularLaptops")]
        public async Task<IActionResult> PopularLaptops()
        {
            var popularLaptops = await _productService.PopularLaptops();

            if (popularLaptops.Count == 0) return NoContent();

            var values = _mapper.Map<List<ProductDTO>>(popularLaptops);

            return Ok(values);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDTO model)
        {
            var product = _mapper.Map<Product>(model);

            ProductValidator validator = new ProductValidator();
            ValidationResult result = await validator.ValidateAsync(product);

            if (result.IsValid)
            {
                if (model.ImageUrl == "")
                {
                    product.ImageUrl = "/img/defaults/default-product.jpg";
                }

                await _productService.Insert(product);

                var values = _mapper.Map<ProductDTO>(product);

                var createdProductImage = new ProductImage
                {
                    ProductId = values.Id,
                    CreatedAt = DateTime.Now,
                    ImageUrl = values.ImageUrl,
                };

                await _productImageService.Insert(createdProductImage);

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

        [HttpPost("AddProductImage")]
        public async Task<IActionResult> AddProductImage(AddProductImageDTO model)
        {
            var productImage = _mapper.Map<ProductImage>(model);

            ProductImageValidator validator = new ProductImageValidator();
            ValidationResult result = await validator.ValidateAsync(productImage);

            if (result.IsValid)
            {
                await _productImageService.Insert(productImage);
                var values = _mapper.Map<ProductImageDTO>(productImage);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetById(id);

            if (product is null) return NotFound();

            await _productService.Delete(product);

            var result = _mapper.Map<ProductDTO>(product);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO model)
        {
            var product = await _productService.GetById(id);

            if (product is null) return NotFound();

            var values = _mapper.Map(model, product);

            ProductValidator validator = new ProductValidator();
            ValidationResult result = await validator.ValidateAsync(values);

            if (result.IsValid)
            {
                await _productService.Update(product);
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
