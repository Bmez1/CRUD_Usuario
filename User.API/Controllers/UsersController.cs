using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using User.Domain.Dtos;
using User.Domain.Dtos.Inputs;
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

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Registra un usuario en el sistema", Description = "Permite registrar un usuario en el sistema.")]
        public async Task<IActionResult> CreateUser(UserCreateInputDto userCreate)
        {
            string response = await _userRepository.CreateUser(userCreate);
            return StatusCode(StatusCodes.Status201Created, response);
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
                return Ok(result);
            }

            return NotFound(new
            {
                Mensaje = "El usuario no existe"
            });
        }


        [HttpPut("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualiza un usuario dado un Id", Description = "Permite actualizar un usuario dado un Id único")]
        public async Task<IActionResult> UpdateUserById([FromBody] UserUpdateInputDto userUpdate, int userId)
        {
            var objUpdate = await _userRepository.GetUserByIdAsync(userId);
            if (objUpdate == null)            
                return BadRequest(new { Mensaje = $"El uusuario de Id {userId} no existe" });
            
            string result = await _userRepository.UpdateUser(userUpdate, userId);
            return Ok(result);
        }

    }
}
