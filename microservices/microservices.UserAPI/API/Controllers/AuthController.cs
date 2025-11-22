using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservices.UserAPI.API.Controllers
{
    [Route("api/Users/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> LogIn([FromBody] LoginRequest request)
        {
            var response = await _authService.LogIn(request);
            return Ok(response);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthResponse>> SignUp([FromBody] UserRequest request)
        {
            var response = await _authService.SignUp(request);
            return Ok(response);
        }
    }
}
