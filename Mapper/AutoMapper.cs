using AutoMapper;
using TaskManagement.DTOs.UserDto;
using TaskManagement.Models;

namespace TaskManagement.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UserModel,GetUserDto>();
            CreateMap<RegisterUserDto,UserModel>();
        }
    }
}
