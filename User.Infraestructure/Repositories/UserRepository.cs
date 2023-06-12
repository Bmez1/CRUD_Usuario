using AutoMapper;
using Dapper;
using System.Data;
using System.Reflection;
using User.Domain.Dtos;
using User.Domain.Dtos.Inputs;
using User.Domain.Interfaces;
using User.Infraestructure.Context;

namespace User.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> CreateUser(UserCreateInputDto user)
        {
            using (var connnection = _context.CreateConnection())
            {
                Domain.Entities.User objUser = _mapper.Map<Domain.Entities.User>(user);
                
                objUser.PassWordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

                string sqlQuery = @"INSERT INTO Users (Title, FirstName, LastName, Email, Role, PasswordHash)
                                    VALUES (@Title, @FirstName, @LastName, @Email, @Role, @PasswordHash)";

                var parameters = new DynamicParameters();

                parameters.Add("Title", objUser.Title, DbType.String);
                parameters.Add("FirstName", objUser.FirstName, DbType.String);
                parameters.Add("LastName", objUser.LastName, DbType.String);
                parameters.Add("Email", objUser.Email, DbType.String);
                parameters.Add("Role", objUser.Role, DbType.Int32);
                parameters.Add("PasswordHash", objUser.PassWordHash, DbType.String);

                int filasAfectadas = await connnection.ExecuteAsync(sqlQuery, parameters);

                return filasAfectadas > 0 ? "Usuario insertado con exito" : "El usuario no fue insertado";
            }
        }

        public async Task<UserDataDto> GetUserByEmailAsync(string email)
        {
            using (var connnection = _context.CreateConnection())
            {
                string sqlQuery = @"SELECT Id UserId, Title, FirstName, LastName, Email, Role 
                                    FROM Users
                                    WHERE Email = @Email;";
                return await connnection.QueryFirstOrDefaultAsync<UserDataDto>(sqlQuery, new { Email = email });
            }
        }

        public async Task<string> UpdateUser(UserUpdateInputDto user, int userId)
        {
            using (var connnection = _context.CreateConnection())
            {
                string sqlQuery = @"UPDATE Users 
                                    SET Title = @Title,
                                        FirstName = @FirstName,
                                        LastName = @LastName, 
                                        Email = @Email, 
                                        Role = @Role 
                                    WHERE Id = @Id";

                var parameters = new DynamicParameters();

                parameters.Add("Title", user.Title, DbType.String);
                parameters.Add("FirstName", user.FirstName, DbType.String);
                parameters.Add("LastName", user.LastName, DbType.String);
                parameters.Add("Email", user.Email, DbType.String);
                parameters.Add("Role", user.Role, DbType.String);
                parameters.Add("Id", userId, DbType.Int32);

                int filasAfectadas = await connnection.ExecuteAsync(sqlQuery, parameters);

                return filasAfectadas > 0 ? "Usuario actualizado con exito" : "El usuario no fue actualizado";
            }
        }

        public async Task<IEnumerable<UserDataDto>> GetUsersAsync()
        {
            using (var connnection = _context.CreateConnection())
            {
                string sqlQuery = @"SELECT Id UserId, Title, FirstName, LastName, Email, Role FROM Users;";
                return await connnection.QueryAsync<UserDataDto>(sqlQuery);
            }
        }

        public async Task<UserDataDto> GetUserByIdAsync(int userId)
        {
            using (var connnection = _context.CreateConnection())
            {
                string sqlQuery = @"SELECT Id UserId, Title, FirstName, LastName, Email, Role 
                                    FROM Users
                                    WHERE Id = @UserId;";
                return await connnection.QueryFirstOrDefaultAsync<UserDataDto>(sqlQuery, new { UserId = userId });
            }
        }
    }
}
