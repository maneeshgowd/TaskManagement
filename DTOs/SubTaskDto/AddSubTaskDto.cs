namespace TaskManagement.DTOs.SubTaskDto
{
    public class AddSubTaskDto
    {
        public string Name { get; set; } = string.Empty;
        public int BoardId { get; set; }
        public int ColumnId { get; set; }
        public int TaskId { get; set; }
    }
}
