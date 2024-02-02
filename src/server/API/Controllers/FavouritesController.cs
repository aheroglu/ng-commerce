using AutoMapper;
using Business.Abstract;
using Entity.Concrete;
using Entity.DTOs.FavouriteDTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouriteService _favouriteService;
        private readonly IMapper _mapper;

        public FavouritesController(IFavouriteService favouriteService, IMapper mapper)
        {
            _favouriteService = favouriteService;
            _mapper = mapper;
        }

        [HttpGet("getfavouritesbyuser/{appUserId}")]
        public async Task<IActionResult> GetFavouritesByUser(int appUserId)
        {
            var favourites = await _favouriteService.GetFavouritesByUser(appUserId);

            if (favourites.Count == 0) return NoContent();

            var values = _mapper.Map<List<FavouriteDTO>>(favourites);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavourite(AddFavouriteDTO model)
        {
            var favourite = _mapper.Map<Favourite>(model);

            await _favouriteService.Insert(favourite);

            var values = _mapper.Map<FavouriteDTO>(favourite);

            return Ok(values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavourite(int id)
        {
            var favourite = await _favouriteService.GetById(id);

            if (favourite is null) return NotFound();

            await _favouriteService.Delete(favourite);

            var values = _mapper.Map<FavouriteDTO>(favourite);

            return Ok(values);
        }
    }
}
