namespace TaskManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<UserModel> Users => Set<UserModel>();
        public DbSet<Board> Boards => Set<Board>();
        public DbSet<BoardTask> Tasks => Set<BoardTask>();
        public DbSet<BoardColumn> Columns => Set<BoardColumn>();
        public DbSet<SubTask> SubTasks => Set<SubTask>();
    }
}
