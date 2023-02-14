namespace TaskManagement.DTOs.TaskDto
{
    public class GetTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<SubTask>? SubTasks { get; set; }
    }
}
