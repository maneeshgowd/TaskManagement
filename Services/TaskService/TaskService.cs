using AutoMapper;
using TaskManagement.DTOs.TaskDto;

namespace TaskManagement.Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TaskService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*
         1. Implement board with columns
         2. Implement tasks with specific columns
         */
        public async Task<ServiceResponse<GetTaskDto>> AddTask(AddTaskDto newTask, BoardColumn column)
        {
            var response = new ServiceResponse<GetTaskDto>();
            var task = _mapper.Map<BoardTask>(newTask);

            try
            {
                var board = await _context.Columns.FirstOrDefaultAsync(b => b.Name == column.Name);

                if (board is null)
                    throw new Exception($"Invalid Board name: {column.Name}!");

                task.BoardColumn = board;

                await _context.Tasks.AddAsync(_mapper.Map<BoardTask>(newTask));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        //public async Task<ServiceResponse<string>> DeleteTask(int id)
        //{
        //    var response = new ServiceResponse<string>();
        //    try
        //    {
        //        var task = await _context.Tasks.Include(t => t.BoardColumn).FirstOrDefaultAsync(task => task.Id == id);

        //        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == task.BoardColumn.)

        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = ex.Message;
        //    }
        //}

        public async Task<ServiceResponse<GetTaskDto>> GetTaskById(int id)
        {
            var response = new ServiceResponse<GetTaskDto>();
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(task => task.Id == id);

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
                Data = await _context.Tasks.Include(task => task.SubTasks).Select(task => _mapper.Map<GetTaskDto>(task)).ToListAsync(),
            };

            return response;
        }

        public Task<ServiceResponse<GetTaskDto>> UpdateTask(AddTaskDto updateTask, int id)
        {
            throw new NotImplementedException();
        }
    }
}
