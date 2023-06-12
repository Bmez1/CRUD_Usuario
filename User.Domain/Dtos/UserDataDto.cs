using System.Text.Json.Serialization;
using User.Domain.Enums;

namespace User.Domain.Dtos
{
    public class UserDataDto
    {
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
    }
}
