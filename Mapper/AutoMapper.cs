using AutoMapper;
using TaskManagement.DTOs.BoardDto;
using TaskManagement.DTOs.SubTaskDto;
using TaskManagement.DTOs.TaskDto;
using TaskManagement.DTOs.UserDto;
using TaskManagement.Models;

namespace TaskManagement.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UserModel, GetUserDto>();
            CreateMap<RegisterUserDto, UserModel>();
            CreateMap<AddUserDto, UserModel>();
            CreateMap<Board, GetBoardDto>().ForMember(dest => dest.BoardName, opt => opt.MapFrom(src => src.Name));
            CreateMap<AddBoardDto, Board>();
            CreateMap<AddBoardDto, GetBoardDto>();
            CreateMap<GetTaskDto, BoardTask>().ReverseMap();
            CreateMap<AddTaskDto, BoardTask>();
            CreateMap<AddTaskDto, GetTaskDto>();
            CreateMap<BoardColumn, GetColumnDto>().ForMember(dest => dest.ColumnName, opt => opt.MapFrom("Name"));
            CreateMap<AddColumnDto, BoardColumn>();
            CreateMap<AddColumnDto, GetColumnDto>();
            CreateMap<SubTask, GetSubTaskDto>();
            CreateMap<AddSubTaskDto, SubTask>();
        }
    }
}
