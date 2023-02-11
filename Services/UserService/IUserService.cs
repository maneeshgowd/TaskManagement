using TaskManagement.DTOs.UserDto;

namespace TaskManagement.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetUsers();
        Task<ServiceResponse<GetUserDto>> GetUserById(int id);
        Task<ServiceResponse<GetUserDto>> UpdateUser(AddUserDto updateUser);
        Task<ServiceResponse<string>> DeleteUser(int id);
    }
}
