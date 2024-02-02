using AutoMapper;
using Business.Abstract;
using Business.ValidationRules;
using Entity.Concrete;
using Entity.DTOs.ProductImageDTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;
        private readonly IMapper _mapper;

        public ProductImagesController(IProductImageService productImageService, IMapper mapper)
        {
            _productImageService = productImageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductImages()
        {
            var productImages = await _productImageService.GetAll();

            if (productImages.Count == 0) return NoContent();

            var result = _mapper.Map<List<ProductImageDTO>>(productImages);

            return Ok(result);
        }

        [HttpGet("GetImagesByProduct/{urlHandle}")]
        public async Task<IActionResult> GetImagesByProduct(string urlHandle)
        {
            var productImage = await _productImageService.ImagesByProduct(urlHandle);

            if (productImage is null) return NotFound();

            var result = _mapper.Map<List<ProductImageDTO>>(productImage);

            return Ok(result);
        }

        [HttpPost]
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
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var productImage = await _productImageService.GetById(id);

            if (productImage is null) return NotFound();

            await _productImageService.Delete(productImage);

            var result = _mapper.Map<ProductImageDTO>(productImage);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductImage(int id, UpdateProductImageDTO model)
        {
            var productImage = await _productImageService.GetById(id);

            if (productImage is null) return NotFound();

            var values = _mapper.Map(model, productImage);

            ProductImageValidator validator = new ProductImageValidator();
            ValidationResult result = await validator.ValidateAsync(values);

            if (result.IsValid)
            {
                await _productImageService.Update(productImage);
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
