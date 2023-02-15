using TaskManagement.Services.UserService;

namespace TaskManagement.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
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

        [HttpPut("update")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUserAsync(AddUserDto updateUser)
        {
            var response = await _service.UpdateUser(updateUser);

            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteUserAsync(int id)
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
