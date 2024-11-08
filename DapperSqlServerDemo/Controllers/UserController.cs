using App.Core.Interfaces;
using App.Core.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DapperSqlServerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            return Ok(await _userRepo.CreateUserAsync(userDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _userRepo.GetAllUserAsync());
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _userRepo.GetUserByIdAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            return Ok(await _userRepo.DeleteUserAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            return Ok(await _userRepo.UpdateUserAsync(userDto));
        }

        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetUsersByName(string name)
        {
            return Ok(await _userRepo.GetUserByNameAsync(name));
        }
    }
}
