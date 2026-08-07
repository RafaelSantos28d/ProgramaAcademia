using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Domain.Entities
{
    public class Plan
    {
        public Plan(int planId, string name, decimal price, int durantionDays)
        {
            PlanId = planId;
            Name = name;
            Price = price;
            DurantionDays = durantionDays;
        }

        public int PlanId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int DurantionDays { get; private set; }
        public ICollection<Enrollment>? Enrollments  { get; private set; }

    }
}
