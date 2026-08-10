using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Academia.Application.DTOs.Enrollment
{
    public class CreateEnrollment
    {
        [Required(ErrorMessage ="Student's id is required")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Plan's id is required")]
        public int PlanId { get; set; }
        [Required(ErrorMessage = "Start date's is required")]
        public DateTime StartDate { get; set;} 
    }
}
