using AutoMapper;

namespace TaskManagement.Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;

        public TaskService(DataContext context, IMapper mapper, IHelper helper)
        {
            _context = context;
            _mapper = mapper;
            _helper = helper;
        }

        public async Task<ServiceResponse<GetTaskDto>> AddTask(AddTaskDto newTask)
        {
            var response = new ServiceResponse<GetTaskDto>();

            try
            {
                var isBoard = await _context.Boards.Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.Id == newTask.BoardId && b.User!.Id == _helper.GetActiveUser());

                var isColumn = await _context.Columns.Include(col => col.User)
                    .SingleOrDefaultAsync(col => col.Id == newTask.ColumnId && col.User!.Id == _helper.GetActiveUser());

                var isTask = await _context.Tasks
                    .Include(task => task.Board)
                    .Include(task => task.User)
                    .SingleOrDefaultAsync(task => task.Title.ToLower() == newTask.Title.ToLower()
                                                  && task.User!.Id == _helper.GetActiveUser() && task.Board!.Id == newTask.BoardId);

                if (isColumn is null)
                    throw new Exception($"Column with the given id:'{newTask.ColumnId}' Not Found!");

                if (isTask is not null)
                    throw new Exception($"Task with the given Title:'{newTask.Title}' already exists!");

                if (isBoard is null)
                    throw new Exception($"Board: '{newTask.BoardId}' does not Exists.!");

                var task = _mapper.Map<BoardTask>(newTask);

                task.Column = isColumn;
                task.Board = isBoard;
                task.User = await _context.Users.FindAsync(_helper.GetActiveUser());

                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetTaskDto>(task);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<string>> DeleteTask(int id)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var task = await _context.Tasks.Include(task => task.User)
                                               .FirstOrDefaultAsync(task => task.Id == id && task.User!.Id == _helper.GetActiveUser());

                if (task is null)
                    throw new Exception($"Task with the given id:'{id}' Not Found!");

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetTaskDto>> GetTaskById(int id)
        {
            var response = new ServiceResponse<GetTaskDto>();
            try
            {
                var task = await _context.Tasks.Include(task => task.User)
                                               .FirstOrDefaultAsync(task => task.Id == id && task.User!.Id == _helper.GetActiveUser());

                if (task is null)
                    throw new Exception($"Task with the given id: {id}, Not Found!");

                response.Data = _mapper.Map<GetTaskDto>(task);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetTaskDto>>> GetTasks()
        {
            var response = new ServiceResponse<List<GetTaskDto>>
            {
                Data = await _context.Tasks.Include(task => task.SubTasks)
                                           .Where(task => task.User!.Id == _helper.GetActiveUser())
                                           .Select(task => _mapper.Map<GetTaskDto>(task)).ToListAsync(),
            };

            return response;
        }

        public Task<ServiceResponse<GetTaskDto>> UpdateTask(AddTaskDto updateTask, int id)
        {
            throw new NotImplementedException();
        }
    }
}
