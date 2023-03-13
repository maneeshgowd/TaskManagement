namespace TaskManagement.Data
{
    public class DataContext : DbContext
    {

        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public virtual DbSet<UserModel> Users => Set<UserModel>();
        public virtual DbSet<Board> Boards => Set<Board>();
        public virtual DbSet<BoardTask> Tasks => Set<BoardTask>();
        public virtual DbSet<BoardColumn> Columns => Set<BoardColumn>();
        public virtual DbSet<SubTask> SubTasks => Set<SubTask>();

    }
}
