using AutoMapper;
using TaskManagement.DTOs.UserDto;

namespace TaskManagement.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetActiveUser() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<string>> DeleteUser(int id)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == GetActiveUser() && user.Id == id);

                if (user is null) throw new Exception($"User with the given id '{id}' Not Found!");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            var response = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user is null) throw new Exception($"User with the given id '{id}' Not Found!");

                response.Data = _mapper.Map<GetUserDto>(user);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetUsers()
        {
            var response = new ServiceResponse<List<GetUserDto>>()
            {
                Data = await _context.Users.Select(user => _mapper.Map<GetUserDto>(user)).ToListAsync(),
            };

            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(int id, RegisterUserDto updateUser)
        {
            var response = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == GetActiveUser() && user.Id == id);

                if (user is null) throw new Exception($"User with the given id '{id}' Not Found!");

                _context.Users.Update(_mapper.Map<UserModel>(updateUser));
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetUserDto>(user);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            

            return response;
        }
    }
}
