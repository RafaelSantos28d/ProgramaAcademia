using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.DTOs.Enrollment
{
    public class UpdateEnrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int PlanId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
