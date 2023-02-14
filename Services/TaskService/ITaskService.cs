using TaskManagement.DTOs.TaskDto;

namespace TaskManagement.Services.TaskService
{
    public interface ITaskService
    {
        Task<ServiceResponse<List<GetTaskDto>>> GetTasks();
        Task<ServiceResponse<GetTaskDto>> GetTaskById(int id);
        Task<ServiceResponse<GetTaskDto>> AddTask(AddTaskDto newTask);
        Task<ServiceResponse<GetTaskDto>> UpdateTask(AddTaskDto updateTask, int id);
        Task<ServiceResponse<string>> DeleteTask(int id);
    }
}
