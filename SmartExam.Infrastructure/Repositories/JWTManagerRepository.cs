using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public JWTManagerRepository(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<GenerateToken> AuthenticateAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("userId", user.Id),
            };

            var roles = await _userManager.GetRolesAsync(user);
            if (roles is not null)
            {
                for (int i = 0; i < roles.Count; i++)
                {
                    claims.Add(new Claim("roles", roles[i]));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["JWT:Audience"],
                Issuer = _configuration["JWT:Issuer"],
                Expires = DateTime.UtcNow.AddDays(5),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            GenerateToken generateToken = new GenerateToken
            {
                //UserId = user.Id,
                Token = tokenHandler.WriteToken(token),
                TokenExpiration = tokenDescriptor.Expires
            };

            return generateToken;
        }
    }
}
