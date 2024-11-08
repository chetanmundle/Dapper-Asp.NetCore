using App.Core.Common.Exceptions;
using App.Core.Interfaces;
using App.Core.Models.User;
using Dapper;
using Domain;
using Infrastructure.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;

        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<User> CreateUserAsync(CreateUserDto user)
        {
            var query = @"INSERT INTO [User] ([Name], Email, City) 
                  VALUES (@Name, @Email, @City);
                  SELECT CAST(SCOPE_IDENTITY() as int);";
            var parameters = new DynamicParameters();
            parameters.Add("Name",user.Name);
            parameters.Add("Email", user.Email);
            parameters.Add("City", user.City);

            using(var connection = _dapperContext.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var userobj = new User
                {
                    UserId = id,
                    Name = user.Name,
                    Email = user.Email,
                    City = user.City,
                };

                return userobj;
            }
        }

        public async Task<string> DeleteUserAsync(int id)
        {
            var query = @"Delete from [User] where UserId = @UserId";
            using(var  conn = _dapperContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, new { UserId = id });
                return "Deleted Successfully";
            }
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            var query = "Select * from [User]";
            using (var con = _dapperContext.CreateConnection())
            {
                var users = await con.QueryAsync<User>(query);
                return users.ToList();
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var query = @"Select * from [User] where UserId = @UserId";
            using var con = _dapperContext.CreateConnection();
            var user = await con.QueryFirstOrDefaultAsync<User>(query,new {UserId = id}) ??
                                 throw new NotFoundException("User with this id Not Found");

            return user;
        }

        public async Task<IEnumerable<User>> GetUserByNameAsync(string userName)
        {
            var procedureName = @"GetUserByName";
            using(var conn = _dapperContext.CreateConnection())
            {
                var users = await conn.QueryAsync<User>(
                                     procedureName,
                                     new { Name = userName },
                                     commandType: CommandType.StoredProcedure
                             );

                if (!users.Any()) throw new NotFoundException("Data Not Found "); 

                return users.ToList();
            }
        }

        public async Task<string> UpdateUserAsync(UserDto user)
        {
            var query = @"Update [User] Set [Name] = @Name, Email = @Email, City = @City where UserId = @UserId";
            var parameter = new DynamicParameters();
            parameter.Add("UserId", user.UserId);
            parameter.Add("Name", user.Name);
            parameter.Add("Email", user.Email);
            parameter.Add("City", user.City);

            using(var conn = _dapperContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, parameter);
                return "User Updated Successfully";
            }
        }

        
    }
}
