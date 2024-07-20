using Application.DTOs.User;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IJWTManagerRepository
    {
        Task<GenerateToken> AuthenticateAsync(User user);
    }
}
