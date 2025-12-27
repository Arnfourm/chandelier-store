using System.Security.Claims;
using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace microservices.UserAPI.API.Controllers
{
    [ApiController]
    [Route("api/Users/[controller]")]
    [Authorize(Roles = "Client")]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouritesService _favouritesService;

        public FavouritesController(IFavouritesService favouritesService)
        {
            _favouritesService = favouritesService;
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<IEnumerable<Guid>>> GetFavourites(Guid userId)
        {
            var favourites = await _favouritesService.GetFavouriteProductsAsync(userId);
            return Ok(favourites);
        }

        [HttpGet("{userId:guid}/count")]
        public async Task<ActionResult<int>> GetFavouritesCount(Guid userId)
        {
            int count = await _favouritesService.GetFavouritesCountAsync(userId);
            return Ok(count);
        }

        [HttpPost]
        public async Task<ActionResult> AddToFavourites([FromQuery] Guid userId, [FromQuery] Guid productId)
        {
            await _favouritesService.AddProductToFavouritesAsync(userId, productId);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveFromFavourites([FromQuery] Guid userId, [FromQuery] Guid productId)
        {
            await _favouritesService.RemoveProductFromFavouritesAsync(userId, productId);
            return Ok();
        }

        [HttpGet("isFavourite")]
        public async Task<ActionResult<bool>> IsProductInFavourites([FromQuery] Guid userId, [FromQuery] Guid productId)
        {
            bool exists = await _favouritesService.IsProductInFavouritesAsync(userId, productId);
            return Ok(exists);
        }
    }
}