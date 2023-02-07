using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs.UserDto;
using TaskManagement.Services.UserService;

namespace TaskManagement.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUsersAsync()
        {
            return Ok(await _service.GetUsers());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserByIdAsync(int id)
        {
            return Ok(await _service.GetUserById(id));
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUserAsync(int id, RegisterUserDto updateUser)
        {
            var response = await _service.UpdateUser(id, updateUser);

            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteUser(int id)
        {
            var response = await _service.DeleteUser(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
