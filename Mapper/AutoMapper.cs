using AutoMapper;
using TaskManagement.DTOs.BoardDto;
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
            CreateMap<AddUserDto,UserModel>();
            CreateMap<BoardDto, Board>().ReverseMap();
            CreateMap<AddBoardDto, Board>();
            CreateMap<AddBoardDto,BoardDto>();
        }
    }
}
