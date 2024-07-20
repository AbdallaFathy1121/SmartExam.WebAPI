using Application.DTOs;
using Application.DTOs.User;
using SmartExam.Application.DTOs.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ApiResponse<RegisterDTO>> RegisterAsync(RegisterDTO dto);
        Task<ApiResponse<GenerateToken>> LoginAsync(LoginDTO dto);
        Task<ApiResponse<string>> LogoutAsync();
        Task<ApiResponse<List<UserDTO>>> GetAllUsersAsync();
        Task<ApiResponse<string>> DeleteUserByIdAsync(DeleteUserDTO dto);
        Task<ApiResponse<UserDTO>> GetUserByIdAsync(string id);
    }
}
