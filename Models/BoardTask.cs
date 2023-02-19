namespace TaskManagement.Models
{
    public class BoardTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public BoardColumn? BoardColumn { get; set; }
        public Board? Board { get; set; }
        public List<SubTask>? SubTasks { get; set; }
        public UserModel? User { get; set; }
    }
}
