using Dapper;
using User.Domain.Dtos;
using User.Domain.Dtos.Inputs;
using User.Domain.Interfaces;
using User.Infraestructure.Context;

namespace User.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public Task CreateUser(UserCreateInputDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDataDto> GetUserByEmailAsync(string email)
        {
            using (var connnection = _context.CreateConnection())
            {
                string sqlQuery = @"SELECT Id, Title, FirstName, LastName, Email, Role 
                                    FROM Users
                                    WHERE Email = @Email;";
                return await connnection.QueryFirstOrDefaultAsync<UserDataDto>(sqlQuery, new { Email = email });
            }
        }

        public Task UpdateUser(UserCreateInputDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDataDto>> GetUsersAsync()
        {
            using (var connnection = _context.CreateConnection())
            {
                string sqlQuery = @"SELECT Id, Title, FirstName, LastName, Email, Role FROM Users;";
                return await connnection.QueryAsync<UserDataDto>(sqlQuery);
            }
        }
    }
}
