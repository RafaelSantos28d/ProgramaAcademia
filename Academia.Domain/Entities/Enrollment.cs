using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Academia.Domain.Entities
{
    public class Enrollment
    {
        public Enrollment(int enrollmentId, int studentId, int planId, DateTime startDate, DateTime endDate)
        {
            EnrollmentId = enrollmentId;
            StudentId = studentId;
            PlanId = planId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int EnrollmentId { get; private set; }
        public int StudentId { get; private set; }
        public Student Student { get; private set; }
        public int PlanId { get; private set; }
        public Plan Plan { get;private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool Late => DateTime.Now > EndDate;
        
    }
}
