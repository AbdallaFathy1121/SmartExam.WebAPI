using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public record UserDTO
    (
        string Id,
        string Email,
        string Name,
        IList<string> Roles
    );
}
