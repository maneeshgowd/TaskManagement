using AutoMapper;
using TaskManagement.DTOs.BoardDto;
using TaskManagement.Services.Helper;

namespace TaskManagement.Services.BoardService
{
    public class BoardService : IBoardService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;

        public BoardService(DataContext context, IMapper mapper, IHelper helper)
        {
            _context = context;
            _mapper = mapper;
            _helper = helper;
        }
        public async Task<ServiceResponse<BoardDto>> AddBoard(BoardDto board)
        {
            var response = new ServiceResponse<BoardDto>();

            try
            {
                var isBoardExists = await _context.Boards.FirstOrDefaultAsync(b => b.Name == board.Name);

                if (isBoardExists is not null)
                    throw new Exception($"Board with the given name: {board.Name} already exists!");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            var newBoard = _mapper.Map<Board>(board);
            newBoard.User = await _context.Users.FirstOrDefaultAsync(user => user.Id == _helper.GetActiveUser());

            _context.Boards.Add(newBoard);
            await _context.SaveChangesAsync();

            response.Data = board;

            return response;
        }

        public async Task<ServiceResponse<string>> DeleteBoard(int id)
        {
            var response = new ServiceResponse<string>();

            try
            {
                var board = await _context.Boards.FirstOrDefaultAsync(board => board.Id == id && board.User!.Id == _helper.GetActiveUser());

                if (board is null)
                    throw new Exception($"Board with the given id '{id}' Not Found!");

                _context.Boards.Remove(board);
                await _context.SaveChangesAsync();

                response.Message = $"Board with the id: {id} Deleted successfully!";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<BoardDto>> GetBoard(int id)
        {
            var response = new ServiceResponse<BoardDto>();
            try
            {
                var board = await _context.Boards.FirstOrDefaultAsync(board => board.Id == id && board.User!.Id == _helper.GetActiveUser());

                if (board is null)
                {
                    throw new Exception($"Board with the given id: {id} Not Found!");
                }

                response.Data = _mapper.Map<BoardDto>(board);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<BoardDto>>> GetBoards()
        {
            var response = new ServiceResponse<List<BoardDto>>
            {
                Data = await _context.Boards.Where(board => board.User!.Id == _helper.GetActiveUser())
                .Select(board => _mapper.Map<BoardDto>(board)).ToListAsync(),
            };
            return response;
        }

        public async Task<ServiceResponse<BoardDto>> UpdateBoard(AddBoardDto updateBoard)
        {
            var response = new ServiceResponse<BoardDto>();
            try
            {
                var board = await _context.Boards.Include(board => board.User)
                    .FirstOrDefaultAsync(board => board.Id == updateBoard.Id && board.Id == _helper.GetActiveUser());

                if (updateBoard is null)
                    throw new Exception($"Board with the given id: {updateBoard?.Id} Not Found!");

                _context.Boards.Update(_mapper.Map<Board>(updateBoard));
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<BoardDto>(updateBoard);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
