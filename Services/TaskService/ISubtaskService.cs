using TaskManagement.DTOs.SubTaskDto;
using TaskManagement.DTOs.TaskDto;

namespace TaskManagement.Services.TaskService
{
    public interface ISubTaskService
    {
        Task<ServiceResponse<List<GetSubTaskDto>>> GetSubTasks();
        Task<ServiceResponse<GetSubTaskDto>> GetSubTaskById(int id);
        Task<ServiceResponse<GetSubTaskDto>> AddSubTask(AddSubTaskDto newSubTask);
        Task<ServiceResponse<GetSubTaskDto>> GetSubTaskById(AddSubTaskDto updateSubTask, int id);
        Task<ServiceResponse<string>> DeleteSubTask(int id);
    }
}
