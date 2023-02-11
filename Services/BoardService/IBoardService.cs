using TaskManagement.DTOs.BoardDto;

namespace TaskManagement.Services.BoardService
{
    public interface IBoardService
    {
        Task<ServiceResponse<List<BoardDto>>> GetBoards();
        Task<ServiceResponse<BoardDto>> GetBoard(int id);
        Task<ServiceResponse<BoardDto>> UpdateBoard(AddBoardDto board);
        Task<ServiceResponse<string>> DeleteBoard(int id);
        Task<ServiceResponse<BoardDto>> AddBoard(BoardDto board);
    }
}
