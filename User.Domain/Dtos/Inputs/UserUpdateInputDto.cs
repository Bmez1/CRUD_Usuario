using System.ComponentModel.DataAnnotations;
using User.Domain.Enums;

namespace User.Domain.Dtos.Inputs
{
    public class UserUpdateInputDto
    {
        public string? Title { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Role Role { get; set; }
    }
}
