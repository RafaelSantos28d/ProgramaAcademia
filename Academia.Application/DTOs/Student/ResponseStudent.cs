using Academia.Application.DTOs.Enrollment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.DTOs.Student
{
    public class ResponseStudent
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }
        public ICollection<ResumoEnrollment> Enrollments { get; set; }
    }
}
