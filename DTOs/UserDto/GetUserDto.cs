using TaskManagement.Models;

namespace TaskManagement.DTOs.UserDto
{
    public class GetUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Gender Gender { get; set; }
    }
}
