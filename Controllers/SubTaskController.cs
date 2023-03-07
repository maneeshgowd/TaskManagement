
using TaskManagement.Services.TaskService;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubTaskController : ControllerBase
    {
        private readonly ISubTaskService _service;

        public SubTaskController(ISubTaskService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetSubTaskDto>>>> GetTaskAsync()
        {
            return Ok(await _service.GetSubTasks());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetSubTaskDto>>> AddTaskAsync(AddSubTaskDto newSubTask)
        {
            var response = await _service.AddSubTask(newSubTask);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetSubTaskDto>>> GetTaskAsync(int id)
        {
            var response = await _service.GetSubTaskById(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetSubTaskDto>>> UpdateTaskAsync(AddSubTaskDto updateSubTask, int id)
        {
            var response = await _service.UpdateSubTask(updateSubTask, id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<ServiceResponse<GetSubTaskDto>>> DeleteTaskAsync(int id)
        {
            var response = await _service.DeleteSubTask(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
