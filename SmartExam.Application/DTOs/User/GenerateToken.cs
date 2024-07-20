using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class GenerateToken
    {
        public string? Token { get; set; }
        public DateTime? TokenExpiration { get; set; }
        public string? UserId { get; set; }
    }
}
