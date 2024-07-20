using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartExam.Application.DTOs.ApiResponse;
using SmartExam.Domain.Entities;
using System;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJWTManagerRepository _jwtRepository;
        public UserService(
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, 
            SignInManager<User> signInManager,
            IJWTManagerRepository jWTManagerRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtRepository = jWTManagerRepository;
        }


        public async Task<ApiResponse<RegisterDTO>> RegisterAsync(RegisterDTO dto)
        {
            ApiResponse<RegisterDTO> response = new ApiResponse<RegisterDTO>();
            try
            {
                if (dto.Password != dto.ConfirmPassword)
                {
                    response.ErrorMessages!.Add("كلمتى المرور يجب ان يكونو متشابها");
                    return response;
                }
                else
                {
                    var exists = await _userManager.FindByEmailAsync(dto.Email);
                    if (exists is null)
                    {
                        var createUser = new User
                        {
                            Email = dto.Email,
                            UserName = dto.Email,
                            Name = dto.Name,
                        };
                    
                        IdentityResult result = await _userManager.CreateAsync(createUser, dto.Password);
                        if (result.Succeeded)
                        {
                            //if (createUser.IsTeacher) 
                            //    await _userManager.AddToRoleAsync(createUser, Roles.Teacher);
                        
                            response.IsSuccess = true;
                            response.Message = "تم اضافة مستخدم جديد بنجاح";
                            response.Data = dto;
                            return response;
                        }
                        else
                        {
                            var errors = result.Errors.Select(a => a.Description);
                            response.ErrorMessages!.AddRange(errors);
                            return response;
                        }
                    }
                    else
                    {
                        response.ErrorMessages!.Add("البريد الالكترونى موجود بالفعل");
                        return response;
                    }
                }
			}
			catch (Exception ex)
			{
                response.ErrorMessages!.Add(ex.Message.ToString());
                return response;
            }
        }

        public async Task<ApiResponse<GenerateToken>> LoginAsync(LoginDTO dto)
        {
            ApiResponse<GenerateToken> response = new ApiResponse<GenerateToken>();
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user is null)
                {
                    response.ErrorMessages!.Add("Invalid Email");
                    return response;
                }
                else
                {
                    bool isValidUser = await _userManager.CheckPasswordAsync(user, dto.Password);
                    if (isValidUser)
                    {
                        SignInResult result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, true);
                        if (result.Succeeded)
                        {
                            GenerateToken generateToken = await _jwtRepository.AuthenticateAsync(user);

                            response.IsSuccess = true;
                            response.Message = "تم تسجيل الدخول بنجاح";
                            response.Data = generateToken;
                            return response;
                        }
                    }
             
                    response.ErrorMessages!.Add("البريد الالكترونى او كلمة المرور خطأ");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessages!.Add(ex.Message);
                return response;
            }
        }

        public async Task<ApiResponse<string>> LogoutAsync()
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                await _signInManager.SignOutAsync();
                
                response.IsSuccess = true;
                response.Message = "تم تسجيل الخروج بنجاح";

                return response;
            }
            catch (Exception ex)
            {
                response.ErrorMessages!.Add(ex.Message);
                return response;
            }
        }
        
        public async Task<ApiResponse<List<UserDTO>>> GetAllUsersAsync()
        {
            ApiResponse<List<UserDTO>> response = new ApiResponse<List<UserDTO>>();
            try
            {
                var users = await _userManager.Users
                    .Select(a=> new UserDTO(a.Id, a.Email!, a.Name, null))
                    .ToListAsync();

                response.IsSuccess = true;
                response.Data = users;
                return response;
            }
            catch (Exception ex)
            {
                response.ErrorMessages!.Add(ex.Message);
                return response;
            }
        }
        
        public async Task<ApiResponse<string>> DeleteUserByIdAsync(DeleteUserDTO dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                var user = await _userManager.FindByIdAsync(dto.id);
                if (user is null)
                {
                    response.ErrorMessages!.Add("لا يوجد مستخدم لحذفة");
                    return response;
                }
                else
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        response.IsSuccess = true;
                        response.Message = "تم الحذف بنجاح";
                        return response;
                    }
                    else
                    {
                        var errors = result.Errors.Select(a => a.Description);
                        response.ErrorMessages!.AddRange(errors);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessages!.Add(ex.Message);
                return response;
            }
        }

        public async Task<ApiResponse<UserDTO>> GetUserByIdAsync(string id)
        {
            ApiResponse<UserDTO> response = new ApiResponse<UserDTO>();
            try
            {
                var result = await _userManager.FindByIdAsync(id);
                if (result is null)
                {
                    response.ErrorMessages!.Add("لا يوجد مستخدم");
                    return response;
                }

                var roles = await _userManager.GetRolesAsync(result);

                var user = new UserDTO (
                    id,
                    result.Email,
                    result.Name,
                    roles
                );

                response.IsSuccess = true;
                response.Data = user;
                return response;
            }
            catch (Exception ex)
            {
                response.ErrorMessages!.Add(ex.Message);
                return response;
            }
        }
        
    }
}
