namespace TaskManagement.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<BoardColumn>? Columns { get; set; }
        public List<BoardTask>? Tasks { get; set; }
        public UserModel? User { get; set; }
    }
}
