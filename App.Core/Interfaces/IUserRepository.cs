using App.Core.Models.User;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(CreateUserDto user);
        Task<string> UpdateUserAsync(UserDto user);
        Task<string> DeleteUserAsync(int id);
        Task<IEnumerable<User>> GetUserByNameAsync(string userName);
    }
}
