using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using TaskManagement.DTOs.BoardDto;
using TaskManagement.Services.BoardService;

namespace TaskManagement.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BoardController : ControllerBase
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<BoardDto>>>> GetBoardsAsync()
    {
        return Ok(await _boardService.GetBoards());
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<BoardDto>>> AddBoardAsync(BoardDto board)
    {
        var response = await _boardService.AddBoard(board);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ServiceResponse<BoardDto>>> GetBoardAsync(int id)
    {
        var response = await _boardService.GetBoard(id);

        if (response is null)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPut("update")]
    public async Task<ActionResult<ServiceResponse<BoardDto>>> UpdateBoardAsync(AddBoardDto board)
    {
        var response = await _boardService.UpdateBoard(board);

        if (response is null)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult<ServiceResponse<BoardDto>>> DeleteBoardAsync(int id)
    {
        var response = await _boardService.DeleteBoard(id);

        if (response is null)
            return NotFound(response);

        return Ok(response);
    }
}
