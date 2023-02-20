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
        public async Task<ServiceResponse<GetBoardDto>> AddBoard(AddBoardDto board)
        {
            var response = new ServiceResponse<GetBoardDto>();

            try
            {
                var isBoardExists = await _context.Boards.FirstOrDefaultAsync(b => b.Name.ToLower() == board.Name.ToLower());

                if (isBoardExists is not null)
                    throw new Exception($"Board with the given name: {board.Name} already exists!");

                var newBoard = _mapper.Map<Board>(board);
                newBoard.User = await _context.Users.FirstOrDefaultAsync(user => user.Id == _helper.GetActiveUser());

                _context.Boards.Add(newBoard);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetBoardDto>(board);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

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

        public async Task<ServiceResponse<GetBoardDto>> GetBoard(int id)
        {
            var response = new ServiceResponse<GetBoardDto>();
            try
            {
                var board = await _context.Boards.Include(board => board.Columns).Include(board => board.Tasks)
                    .FirstOrDefaultAsync(board => board.Id == id && board.User!.Id == _helper.GetActiveUser());

                if (board is null)
                {
                    throw new Exception($"Board with the given id: {id} Not Found!");
                }

                response.Data = _mapper.Map<GetBoardDto>(board);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetBoardDto>>> GetBoards()
        {
            var response = new ServiceResponse<List<GetBoardDto>>
            {
                Data = await _context.Boards.Include(board => board.Columns).Include(board => board.Tasks)
                .Where(board => board.User!.Id == _helper.GetActiveUser())
                .Select(board => _mapper.Map<GetBoardDto>(board)).ToListAsync(),
            };
            return response;
        }

        public async Task<ServiceResponse<GetBoardDto>> UpdateBoard(AddBoardDto updateBoard, int id)
        {
            var response = new ServiceResponse<GetBoardDto>();
            try
            {
                var board = await _context.Boards.FirstOrDefaultAsync(board => board.Id == id);

                if (board is null)
                    throw new Exception($"Board with the given id: {id} Not Found!");

                _context.Boards.Update(_mapper.Map<Board>(updateBoard));
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetBoardDto>(updateBoard);

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
