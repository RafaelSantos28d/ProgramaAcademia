using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.DTOs.Student
{
    public class UpdateDTO
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
    }
}
