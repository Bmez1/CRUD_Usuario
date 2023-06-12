using User.Domain.Dtos;
using User.Domain.Dtos.Inputs;

namespace User.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDataDto>> GetUsersAsync();
        Task<UserDataDto> GetUserByEmailAsync(string email);
        Task CreateUser(UserCreateInputDto user);
        Task UpdateUser(UserCreateInputDto user);
    }
}
