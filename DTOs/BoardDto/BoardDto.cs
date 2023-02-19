namespace TaskManagement.DTOs.BoardDto
{
    public class BoardDto
    {
        public string Name { get; set; } = string.Empty;
        public List<GetColumnDto>? Columns { get; set; } 
    }
}
