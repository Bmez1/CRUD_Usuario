using User.Domain.Dtos;
using User.Domain.Dtos.Inputs;

namespace User.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDataDto>> GetUsersAsync();
        Task<UserDataDto> GetUserByEmailAsync(string email);
        Task<UserDataDto> GetUserByIdAsync(int userId);
        Task<string> CreateUser(UserCreateInputDto user);
        Task<string> UpdateUser(UserUpdateInputDto user, int userId);
    }
}
