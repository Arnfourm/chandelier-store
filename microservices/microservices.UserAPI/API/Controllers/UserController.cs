using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservices.UserAPI.API.Controllers {

    [ApiController]
    [Route("api/Users/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            IEnumerable<UserResponse> response = await _userService.GetAllUsers();

            return Ok(response);
        }
    }
}