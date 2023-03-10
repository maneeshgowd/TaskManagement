namespace TaskManagement.Models
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Board? Board { get; set; }
        public BoardColumn? Column { get; set; }
        public BoardTask? Task { get; set; }
        public UserModel? User { get; set; }
    }
}
