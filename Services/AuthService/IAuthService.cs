
namespace TaskManagement.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(RegisterUserDto newUser);
        Task<ServiceResponse<string>> Login(LoginUserDto loginUser);
        Task<bool> UserExists(string email);
    }
}
