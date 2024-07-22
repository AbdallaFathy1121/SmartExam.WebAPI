using Application.Interfaces.Repositories;
using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using SmartExam.Application.Interfaces.Services;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Infrastructure.Services
{
    public class InitialService : IInitialService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public InitialService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            if (!await _roleManager.RoleExistsAsync(Roles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Roles.Teacher));
            }

            var adminEmail = "admin@admin.com";
            var adminName = "Admin Admin";
            if (await _userManager.FindByEmailAsync(adminEmail) == null)
            {
                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    Name = adminName
                };

                await _userManager.CreateAsync(user, "Admin1121");
                await _userManager.AddToRoleAsync(user, Roles.Admin);
            }
        }
    }
}
