namespace TaskManagement.DTOs.TaskDto
{
    public class AddTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ColumnId { get; set; }
        public int BoardId { get; set; }

    }
}
