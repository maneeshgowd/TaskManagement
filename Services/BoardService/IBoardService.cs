using TaskManagement.DTOs.BoardDto;

namespace TaskManagement.Services.BoardService
{
    public interface IBoardService
    {
        Task<ServiceResponse<List<GetBoardDto>>> GetBoards();
        Task<ServiceResponse<GetBoardDto>> GetBoard(int id);
        Task<ServiceResponse<GetBoardDto>> UpdateBoard(AddBoardDto board, int id);
        Task<ServiceResponse<string>> DeleteBoard(int id);
        Task<ServiceResponse<GetBoardDto>> AddBoard(AddBoardDto board);
    }
}
