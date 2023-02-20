using AutoMapper.Configuration.Annotations;
using TaskManagement.DTOs.TaskDto;

namespace TaskManagement.DTOs.BoardDto
{
    public class GetBoardDto
    {
        public string BoardName { get; set; } = string.Empty;
        public List<GetColumnDto>? Columns { get; set; } 
        public List<GetTaskDto>? Tasks { get; set; }
    }
}
