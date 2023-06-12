using System.Text.Json.Serialization;

namespace User.Domain.Dtos
{
    public class UserDataDto
    {
        [JsonPropertyName("usuarioId")]
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? FirtsName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
    }
}
