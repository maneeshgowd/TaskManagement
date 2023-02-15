using TaskManagement.Services.ColumnService;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ColumnsController : ControllerBase
    {
        private readonly IColumnService _service;

        public ColumnsController(IColumnService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetColumnDto>>>> GetColumnsAsync()
        {
            return Ok(await _service.GetColumns());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetColumnDto>>> AddColumnAsync(AddColumnDto newColumn)
        {
            var response = await _service.AddColumn(newColumn);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id:int")]
        public async Task<ActionResult<ServiceResponse<GetColumnDto>>> GetColumnAsync(int id)
        {
            var response = await _service.GetColumn(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut("update/{id:int")]
        public async Task<ActionResult<ServiceResponse<GetColumnDto>>> UpdateColumnAsync(AddColumnDto updateColumn, int id)
        {
            var response = await _service.UpdateColumn(updateColumn, id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete/{id:int")]
        public async Task<ActionResult<ServiceResponse<GetColumnDto>>> DeleteColumnAsync(int id)
        {
            var response = await _service.DeleteColumn(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
