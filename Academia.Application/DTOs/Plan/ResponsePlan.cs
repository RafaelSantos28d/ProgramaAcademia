using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Application.DTOs.Plan
{
    public class ResponsePlan
    {
        public int PlanId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
    }
}
