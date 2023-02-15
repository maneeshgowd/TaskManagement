using TaskManagement.DTOs.ColumnDto;

namespace TaskManagement.Services.ColumnService
{
    public interface IColumnService
    {
        Task<ServiceResponse<List<GetColumnDto>>> GetColumns();
        Task<ServiceResponse<GetColumnDto>> GetColumn(int id);
        Task<ServiceResponse<GetColumnDto>> AddColumn(AddColumnDto newColumn);
        Task<ServiceResponse<GetColumnDto>> UpdateColumn(AddColumnDto updateColumn, int id);
        Task<ServiceResponse<string>> DeleteColumn(int id);
    }
}
