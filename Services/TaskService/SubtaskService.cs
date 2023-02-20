using TaskManagement.DTOs.SubTaskDto;

namespace TaskManagement.Services.TaskService
{
    public class SubTaskService : ISubTaskService
    {
        public Task<ServiceResponse<GetSubTaskDto>> AddSubTask(AddSubTaskDto newSubTask)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<string>> DeleteSubTask(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetSubTaskDto>> GetSubTaskById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetSubTaskDto>> GetSubTaskById(AddSubTaskDto updateSubTask, int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetSubTaskDto>>> GetSubTasks()
        {
            throw new NotImplementedException();
        }
    }
}
