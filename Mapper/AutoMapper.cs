using AutoMapper;

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
            CreateMap<AddBoardDto, GetBoardDto>().ForMember(dest => dest.BoardName, opt => opt.MapFrom(src => src.Name));
            CreateMap<GetTaskDto, BoardTask>().ReverseMap();
            CreateMap<AddTaskDto, BoardTask>();
            CreateMap<AddTaskDto, GetTaskDto>();
            CreateMap<BoardColumn, GetColumnDto>().ForMember(dest => dest.ColumnName, opt => opt.MapFrom("Name"));
            CreateMap<AddColumnDto, BoardColumn>();
            CreateMap<AddColumnDto, GetColumnDto>().ForMember(dest => dest.ColumnName, opt => opt.MapFrom("Name"));
            CreateMap<SubTask, GetSubTaskDto>();
            CreateMap<AddSubTaskDto, SubTask>();
        }
    }
}
