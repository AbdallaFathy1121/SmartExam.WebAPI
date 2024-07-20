using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Services;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Exam_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        // GET: api/Users
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        // GET: api/Users/{Id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        // POST api/Users/Register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        // POST api/Users/Login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO dto)
        {
            var result = await _userService.LoginAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            else 
                return BadRequest(result);
        }

        // POST api/Users/Delete
        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteUserAsync([FromBody]DeleteUserDTO dto)
        {
            var result = await _userService.DeleteUserByIdAsync(dto);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        // POST api/Users/Logout
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            var result = await _userService.LogoutAsync();
            if (result.IsSuccess)
                return Ok(result);
            else 
                return BadRequest(result);
        }

    }
}
