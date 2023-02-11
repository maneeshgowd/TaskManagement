using TaskManagement.Services.AuthService;

namespace TaskManagement.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<string>>> RegisterAsync(RegisterUserDto newUser)
    {
        var response = await _service.Register(newUser);

        if (response.Data is null)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> LoginAsync(LoginUserDto loginUser)
    {
        return Ok(await _service.Login(loginUser));
    }
}
