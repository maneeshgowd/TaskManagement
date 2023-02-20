namespace TaskManagement.Models
{
    public class BoardColumn
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Board? Board { get; set; }
        public List<BoardTask>? Tasks { get; set; }
        public List<SubTask>? SubTasks { get; set; }
        public UserModel? User { get; set; }

    }
}
