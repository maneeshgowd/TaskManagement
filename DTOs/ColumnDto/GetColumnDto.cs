using TaskManagement.DTOs.TaskDto;

namespace TaskManagement.DTOs.ColumnDto
{
    public class GetColumnDto
    {
        public string ColumnName { get; set; } = string.Empty;
        public List<GetTaskDto>? Tasks { get; set; }
    }
}
