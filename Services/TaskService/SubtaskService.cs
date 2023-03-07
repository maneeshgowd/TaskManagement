using AutoMapper;

namespace TaskManagement.Services.TaskService
{
    public class SubTaskService : ISubTaskService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHelper _helper;

        public SubTaskService(IMapper mapper, DataContext context, IHelper helper)
        {
            _mapper = mapper;
            _context = context;
            _helper = helper;
        }

        public async Task<ServiceResponse<GetSubTaskDto>> AddSubTask(AddSubTaskDto newSubTask)
        {
            var response = new ServiceResponse<GetSubTaskDto>();
            try
            {
                var isSubTask = await _context.SubTasks.Include(s => s.Task)
                                                       .SingleOrDefaultAsync(s => s.Name.ToLower() == newSubTask.Name.ToLower() && s.Task!.Id == newSubTask.TaskId);

                var isUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == _helper.GetActiveUser());

                var isBoard = await _context.Boards.Include(b => b.User)
                                                   .SingleOrDefaultAsync(b => b.Id == newSubTask.BoardId && b.User!.Id == isUser!.Id);

                var isColumn = await _context.Columns.Include(col => col.User)
                                                     .Include(col => col.Board)
                                                     .SingleOrDefaultAsync(col => col.Id == newSubTask.ColumnId && col.Board!.Id == isBoard!.Id && col.User!.Id == isUser!.Id);

                var isTask = await _context.Tasks.Include(task => task.User)
                                                 .Include(task => task.Board)
                                                 .Include(task => task.Column)
                                                 .SingleOrDefaultAsync(task => task.Id == newSubTask.TaskId && task.User!.Id == isUser!.Id && task.Board!.Id == isBoard!.Id && task.Column!.Id == isColumn!.Id);

                if (isSubTask is not null)
                    throw new Exception($"Sub Task with the given name: {newSubTask.Name}. Already Exists!");

                if (isBoard is null)
                    throw new Exception($"Board with the given id: {newSubTask.BoardId}. Not Found!");

                if (isColumn is null)
                    throw new Exception($"Column with the given id: {newSubTask.ColumnId}. Not Found!");

                if (isTask is null)
                    throw new Exception($"Task with the given id: {newSubTask.TaskId}. Not Found!");

                var subTask = _mapper.Map<SubTask>(newSubTask);
                subTask.Column = isColumn;
                subTask.Board = isBoard;
                subTask.User = isUser;
                subTask.Task = isTask;

                await _context.AddAsync(subTask);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetSubTaskDto>(subTask);

            }
            catch (Exception ex)
            {
                _helper.SetHttpErrorResponse(response, ex.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<string>> DeleteSubTask(int id)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var subTask = await _context.SubTasks.Include(st => st.User)
                                                     .SingleOrDefaultAsync(st => st.User!.Id == _helper.GetActiveUser() && st.Id == id);

                if (subTask is null)
                    throw new Exception($"SubTask with the given id: {id}. Not Found!");

                _context.Remove(subTask);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _helper.SetHttpErrorResponse(response, ex.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<GetSubTaskDto>> GetSubTaskById(int id)
        {
            var response = new ServiceResponse<GetSubTaskDto>();

            try
            {
                var subTask = await _context.SubTasks.Include(st => st.User)
                                                     .SingleOrDefaultAsync(st => st.User!.Id == _helper.GetActiveUser() && st.Id == id);

                if (subTask is null)
                    throw new Exception($"SubTask with the given id: {id}. Not Found!");

                response.Data = _mapper.Map<GetSubTaskDto>(subTask);
            }
            catch (Exception ex)
            {
                _helper.SetHttpErrorResponse(response, ex.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<GetSubTaskDto>> UpdateSubTask(AddSubTaskDto updateSubTask, int id)
        {
            var response = new ServiceResponse<GetSubTaskDto>();

            try
            {
                var subtask = await _context.SubTasks
                                               .Include(stask => stask.User)
                                               .Include(stask => stask.Board)
                                               .Include(stask => stask.Column)
                                               .Include(stask => stask.Task)
                                               .FirstOrDefaultAsync(stask => stask.User!.Id == _helper.GetActiveUser() && stask.Id == id);

                if (subtask is null)
                    throw new Exception($"Task with the given id: {id} Not Found!");

                var updatedSubTask = _mapper.Map<SubTask>(updateSubTask);
                updatedSubTask.Board = subtask.Board;
                updatedSubTask.Column = subtask.Column;
                updatedSubTask.Task= subtask.Task;

                _context.SubTasks.Update(updatedSubTask);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetSubTaskDto>(updatedSubTask);
            }
            catch (Exception ex)
            {
                _helper.SetHttpErrorResponse(response, ex.Message);
            }

            return response;

        }

        public async Task<ServiceResponse<List<GetSubTaskDto>>> GetSubTasks()
        {
            var response = new ServiceResponse<List<GetSubTaskDto>>
            {
                Data = await _context.SubTasks.Include(s => s.User).Where(st => st.User!.Id == _helper.GetActiveUser())
                                                                   .Select(st => _mapper.Map<GetSubTaskDto>(st)).ToListAsync(),
            };

            return response;
        }
    }
}
