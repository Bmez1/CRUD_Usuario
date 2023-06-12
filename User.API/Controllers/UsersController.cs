using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using User.Domain.Dtos;
using User.Domain.Interfaces;

namespace User.API.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDataDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtiene todos los usuarios del sistema", Description = "Permite obtener todos los usuarios del sistema.")]
        public async Task<IActionResult> GetUsers() 
            => Ok(await _userRepository.GetUsersAsync());



        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDataDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtiene un usuario por su Email", Description = "Permite obtener un usuario de acuerdo a su email.")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            UserDataDto? result = await _userRepository.GetUserByEmailAsync(email);

            if (result != null)
            {
                Ok(result);
            }

            return NotFound(new
            {
                Mensaje = "El usuario no existe"
            });
        }

    }
}
