namespace TaskManagement.Models
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public BoardTask? Task { get; set; }
    }
}
