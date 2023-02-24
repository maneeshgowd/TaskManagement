using TaskManagement.Services.TaskService;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetTaskDto>>>> GetTaskAsync()
        {
            return Ok(await _service.GetTasks());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetTaskDto>>> AddTaskAsync(AddTaskDto newTask)
        {
            var response = await _service.AddTask(newTask);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetTaskDto>>> GetTaskAsync(int id)
        {
            var response = await _service.GetTaskById(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetTaskDto>>> UpdateTaskAsync(AddTaskDto updateTask, int id)
        {
            var response = await _service.UpdateTask(updateTask, id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetTaskDto>>> DeleteTaskAsync(int id)
        {
            var response = await _service.DeleteTask(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
