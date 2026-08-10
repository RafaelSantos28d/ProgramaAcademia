using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Academia.Application.DTOs.Plan
{
    public class CreatePlan
    {
        [MaxLength(250)]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Range(0.01,99999999999.99,ErrorMessage="Invalid price")]
        public decimal Price { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="The number must be positive")]
        public int DurationDays { get; set; }
    }
}
