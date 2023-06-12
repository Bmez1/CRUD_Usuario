using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System.Data;
using User.Domain.Helper;

namespace User.Infraestructure.Context
{
    public class DataContext
    {
        private readonly InfoDataBase _infoDataBase;
        public DataContext(IOptions<InfoDataBase> infoDataBase)
        {
            _infoDataBase = infoDataBase.Value;   
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(_infoDataBase.StringConnection);
        }

        public async Task Init()
        {
            // create database tables if they don't exist
            using var connection = CreateConnection();
            await _initUsers();

            async Task _initUsers()
            {
                string sql = @"
                    CREATE TABLE IF NOT EXISTS 
                    Users (
                        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        Title TEXT,
                        FirstName TEXT,
                        LastName TEXT,
                        Email TEXT,
                        Role INTEGER,
                        PasswordHash TEXT
                    );
                ";
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
