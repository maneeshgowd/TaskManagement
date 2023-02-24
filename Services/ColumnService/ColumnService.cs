using AutoMapper;

namespace TaskManagement.Services.ColumnService
{
    public class ColumnService : IColumnService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;

        public ColumnService(DataContext context, IMapper mapper, IHelper helper)
        {
            _context = context;
            _mapper = mapper;
            _helper = helper;
        }
        public async Task<ServiceResponse<GetColumnDto>> AddColumn(AddColumnDto newColumn)
        {
            var response = new ServiceResponse<GetColumnDto>();

            try
            {
                var board = await _context.Boards.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == newColumn.BoardId && b.User!.Id == _helper.GetActiveUser());

                var isColumn = await _context.Columns
                    .Include(c => c.Board)
                    .Include(c => c.User)
                    .SingleOrDefaultAsync(c => c.Name.ToLower() == newColumn.Name.ToLower() && c.Board!.Id == newColumn.BoardId && c.User!.Id == _helper.GetActiveUser());
                

                if (isColumn is not null)
                    throw new Exception($"Column: '{isColumn.Name}' alreay exists in the board.");

                if (board is null)
                    throw new Exception($"Board: '{newColumn.BoardId}' does not Exists.!");

                var column = _mapper.Map<BoardColumn>(newColumn);

                column.Board = board;
                column.User = board.User;

                await _context.Columns.AddAsync(column);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetColumnDto>(newColumn);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<string>> DeleteColumn(int id)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var isColumn = await _context.Columns.Include(col => col.User).FirstOrDefaultAsync(col => col.Id == id && col.User!.Id == _helper.GetActiveUser());

                if (isColumn is null)
                    throw new Exception($"Task with the given id: {id} Not Found!");

                _context.Columns.Remove(isColumn);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetColumnDto>>> GetColumns()
        {
            var response = new ServiceResponse<List<GetColumnDto>>
            {
                Data = await _context.Columns.Include(col => col.Tasks).Where(col => col.User!.Id == _helper.GetActiveUser())
                .Select(col => _mapper.Map<GetColumnDto>(col)).ToListAsync(),
            };

            return response;
        }

        public async Task<ServiceResponse<GetColumnDto>> GetColumn(int id)
        {
            var response = new ServiceResponse<GetColumnDto>();

            try
            {

                var column = await _context.Columns.Include(col => col.Tasks)
                                                   .SingleOrDefaultAsync(c => c.Id.Equals(id) && c.User!.Id == _helper.GetActiveUser());

                if (column is null)
                    throw new Exception($"Column with the specified id: {id}, Not Found!");

                response.Data = _mapper.Map<GetColumnDto>(column);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<GetColumnDto>> UpdateColumn(AddColumnDto updateColumn, int id)
        {
            var response = new ServiceResponse<GetColumnDto>();
            try
            {
                var isBoard = await _context.Boards.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == updateColumn.BoardId && b.User!.Id == _helper.GetActiveUser());

                var isColumn = await _context.Columns.FindAsync(id);


                if (isBoard is null)
                    throw new Exception($"Board: {updateColumn.BoardId} does not Exists.!");

                if (isColumn is null)
                    throw new Exception($"Column with the given id: {id} Not Found!");

                var column = _mapper.Map<BoardColumn>(updateColumn);
                column.Board = isBoard;
                column.Id = id;

                _context.Columns.Update(column);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetColumnDto>(updateColumn);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
