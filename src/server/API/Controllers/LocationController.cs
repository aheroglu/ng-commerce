using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;

        public LocationController(ICityService cityService, IDistrictService districtService)
        {
            _cityService = cityService;
            _districtService = districtService;
        }

        [HttpGet("getcities")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _cityService.GetAll();

            if (cities.Count == 0) return NoContent();

            return Ok(cities);
        }

        [HttpGet("getdistricts")]
        public async Task<IActionResult> GetDistricts()
        {
            var distrcits = await _districtService.GetAll();

            if (distrcits.Count == 0) return NoContent();

            return Ok(distrcits);
        }

        [HttpGet("getdistricts/{cityId}")]
        public async Task<IActionResult> GetDistrict(int cityId)
        {
            var distrcit = await _districtService.GetDistrictsByCity(cityId);

            if (distrcit is null) return NotFound();

            return Ok(distrcit);
        }
    }
}
