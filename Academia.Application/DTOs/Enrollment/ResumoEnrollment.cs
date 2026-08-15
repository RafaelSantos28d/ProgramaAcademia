using Academia.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.DTOs.Enrollment
{
    public class ResumoEnrollment
    {
        public int EnrollmentId { get; set; }
        public string PlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EnrollmentSatatus EnrollmentSatatus{ get; set; }
        public bool Late => DateTime.Now > EndDate;
    }
}
