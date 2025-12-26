using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace microservices.UserAPI.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
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
            try
            {
                IEnumerable<UserResponse> response = await _userService.GetAllUsersAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
        {
            try
            {
                UserResponse response = await _userService.GetSingleUserByIdAsync(id);
                return Ok(response);
            }
            catch (ArgumentException ex) when (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserResponse>> GetUserByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest(new { message = "Email cannot be empty" });

                UserResponse response = await _userService.GetSingleUserByEmailAsync(email);
                return Ok(response);
            }
            catch (ArgumentException ex) when (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { message = "Request cannot be null" });

                if (string.IsNullOrWhiteSpace(request.Email))
                    return BadRequest(new { message = "Email is required" });

                if (string.IsNullOrWhiteSpace(request.Password))
                    return BadRequest(new { message = "Password is required" });

                var userId = await _userService.CreateUserAsync(request);
                return CreatedAtAction(nameof(GetUserById), new { id = userId },
                    new
                    {
                        message = "User created successfully",
                        userId = userId
                    });
            }
            catch (ArgumentException ex) when (ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { message = "Request cannot be null" });

                if (!string.IsNullOrWhiteSpace(request.Email))
                {
                    try
                    {
                        var email = new System.Net.Mail.MailAddress(request.Email);
                    }
                    catch
                    {
                        return BadRequest(new { message = "Invalid email format" });
                    }
                }

                await _userService.UpdateUserAsync(request);
                return Ok(new { message = "User updated successfully" });
            }
            catch (ArgumentException ex) when (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex) when (ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserByIdAsync(id);
                return Ok(new { message = "User deleted successfully" });
            }
            catch (ArgumentException ex) when (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }
    }
}