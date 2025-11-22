using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace microservices.UserAPI.API.Controllers {

    [ApiController]
    [Authorize]
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

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
        {
            UserResponse response = await _userService.GetSingleUserById(id);
            return Ok(response);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserResponse>> GetUserByEmail(string email)
        {
            UserResponse response = await _userService.GetSingleUserByEmail(email);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserRequest request)
        {
            await _userService.CreateNewUser(request);
            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserRequest request)
        {
            var updateRequest = request with { Id = id };
            await _userService.UpdateUser(updateRequest);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteSingleUserById(id);
            return Ok();
        }
    }
}