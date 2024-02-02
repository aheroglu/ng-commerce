using AutoMapper;
using Business.Abstract;
using Business.ValidationRules;
using Entity.Concrete;
using Entity.DTOs.CategoryDTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAll();

            if (categories.Count is 0) return NoContent();

            var result = _mapper.Map<List<CategoryDTO>>(categories);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category is null) return NotFound();

            var result = _mapper.Map<CategoryDTO>(category);

            return Ok(result);
        }

        [HttpGet("GetCategoryByUrlHandle/{urlHandle}")]
        public async Task<IActionResult> GetCategoryByUrlHandle(string urlHandle)
        {
            var category = await _categoryService.GetByUrlHandle(urlHandle);

            if (category is null) return NotFound();

            var result = _mapper.Map<CategoryDTO>(category);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);

            CategoryValidator validator = new CategoryValidator();
            ValidationResult result = await validator.ValidateAsync(category);

            if (result.IsValid)
            {
                await _categoryService.Insert(category);
                return Ok(category);
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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category is null) return NotFound();

            await _categoryService.Delete(category);

            var result = _mapper.Map<CategoryDTO>(category);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto model)
        {
            var category = await _categoryService.GetById(id);

            if (category is null) return NotFound();

            var values = _mapper.Map(model, category);

            CategoryValidator validator = new CategoryValidator();
            ValidationResult result = await validator.ValidateAsync(values);

            if (result.IsValid)
            {
                await _categoryService.Update(values);
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
