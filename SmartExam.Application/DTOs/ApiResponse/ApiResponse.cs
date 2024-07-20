using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartExam.Application.DTOs.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; } = false;
        public T? Data { get; set; }
        public string? Message { get; set; }
        public List<string>? ErrorMessages { get; set; } = new List<string>();
    }
}