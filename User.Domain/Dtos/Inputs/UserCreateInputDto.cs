using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace User.Domain.Dtos.Inputs
{
    public class UserCreateInputDto
    {
        [JsonPropertyName("titulo")]
        public string? Title { get; set; }
        [JsonPropertyName("nombres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FirtsName { get; set; }
        [JsonPropertyName("apellidos")]
        public string? LastName { get; set; }
        [JsonPropertyName("correoElectronico")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [JsonPropertyName("rol")]
        public Role Role { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [JsonPropertyName("claveAcceso")]
        public string PassWordHash { get; set; }
    }
}
