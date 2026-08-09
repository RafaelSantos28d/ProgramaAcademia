using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Academia.Domain.Entities
{
    public class Plan
    {
        public Plan(int planId, string name, decimal price, int durationDays)
        {
            PlanId = planId;
            Name = name;
            Price = price;
            DurationDays = durationDays;
        }
        public Plan() { }

        public int PlanId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int DurationDays { get; private set; }
        public ICollection<Enrollment>? Enrollments  { get; private set; }

        public void AlterarDados(string name, decimal price, int durationDays)
        {
            
            Name = name;
            Price = price;
            DurationDays = durationDays;
        }

    }
}
