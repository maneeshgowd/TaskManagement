using TaskManagement.Services.BoardService;

namespace TaskManagement.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class BoardsController : ControllerBase
{
    private readonly IBoardService _boardService;

    public BoardsController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetBoardDto>>>> GetBoardsAsync()
    {
        return Ok(await _boardService.GetBoards());
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetBoardDto>>> AddBoardAsync(AddBoardDto board)
    {
        var response = await _boardService.AddBoard(board);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ServiceResponse<GetBoardDto>>> GetBoardAsync(int id)
    {
        var response = await _boardService.GetBoard(id);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPut("update/{id:int}")]
    public async Task<ActionResult<ServiceResponse<GetBoardDto>>> UpdateBoardAsync(AddBoardDto board, int id)
    {
        var response = await _boardService.UpdateBoard(board, id);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult<ServiceResponse<GetBoardDto>>> DeleteBoardAsync(int id)
    {
        var response = await _boardService.DeleteBoard(id);

        if (!response.Success)
            return NotFound(response);

        return NoContent();
    }
}
