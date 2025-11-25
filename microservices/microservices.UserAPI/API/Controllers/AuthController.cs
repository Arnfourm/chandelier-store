using System.Security.Claims;
using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("SignUp")]
        public async Task<ActionResult<AuthResponse>> SignUp([FromBody] UserRequest request)
        {
            var response = await _authService.SignUp(request);
            return Ok(response);
        }

        [HttpPost("LogIn")]
        public async Task<ActionResult<AuthResponse>> LogIn([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.LogIn(request);
                return Ok(response);
            }
            catch (Exception ex) when (ex.Message.Contains("email"))
            {
                return Unauthorized(new { message = "Пользователь с таким email не найден" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("LogOut")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var success = await _authService.LogOut(Guid.Parse(userId));
            return Ok();
        }


        [HttpPost("RefreshToken")]
        public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var response = await _authService.RefreshToken(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
